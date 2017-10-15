using System.Net.Http.Formatting;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace NopStarterKit.Web.Controllers
{
    [Tree("nopdashboard", "entities", "Nop Dashboard")]
    [PluginController("NopDashboard")]
    public class NopEntitiesTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
           
            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            TreeNodeCollection nodes = new TreeNodeCollection();
            
            if (id == "-1")
            {
                nodes.Add(CreateTreeNode("products", id, queryStrings, "Products", "icon-book", "nopdashboard/entities/products/all"));

                nodes.Add(CreateTreeNode("orders", id, queryStrings, "Orders", "icon-time", "nopdashboard/entities/orders/all"));
            }

            return nodes;
        }
    }
}