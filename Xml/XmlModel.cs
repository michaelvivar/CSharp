using System.IO;
using System.Web.Hosting;
using System.Xml.Linq;

namespace Application.Models
{
    public XDocument Load(string file)
    {
        string filepath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"App_Data\" + file);
        return XDocument.Load(fielpath);
    }
    
    public void Save(XDocument xml, string file)
    {
        string filepath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"App_Data\" + file);
        xml.Save(filepath);
    }
}
