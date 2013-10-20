using System.Collection.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Application.Models
{
    public class XmlListmenu : XmlModel
    {
        private XDocument xml;
    
        public XmlListmenu()
        {
            xml = this.Load("XmlListmenu.xml");
        }
        
        public List<SelectListItem> Gender()
        {
            List<SelectListItem> list = (from node in xml.Descendants("Gender").Descendants("option")
                                         select new SelectListItem
                                         {
                                            Text = node.Value,
                                            Value = node.Attribute("value").Value
                                         }).ToList();
            return list;
        }
        
        public List<SelectListItem> CivilStatus()
        {
            List<SelectListItem> list = (from node in xml.Descendants("CivilStatus").Descendants("option")
                                         select new SelectListItem
                                         {
                                            Text = node.Value,
                                            Value = node.Attribute("value").Value
                                         }).ToList();
            return list;
        }
    }
}
