public static class Helper
    {
        private static readonly IDictionary<Type, IEnumerable<PropertyInfo>> _Properties =
            new Dictionary<Type, IEnumerable<PropertyInfo>>();

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static IEnumerable<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var objType = typeof(T);
                IEnumerable<PropertyInfo> properties;

                lock (_Properties)
                {
                    if (!_Properties.TryGetValue(objType, out properties))
                    {
                        properties = objType.GetProperties().Where(property => property.CanWrite);
                        _Properties.Add(objType, properties);
                    }
                }

                var list = new List<T>(table.Rows.Count);

                foreach (var row in table.AsEnumerable().Skip(1))
                {
                    var obj = new T();

                    foreach (var prop in properties)
                    {
                        try
                        {
                            prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType), null);
                        }
                        catch
                        {
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }
    }
