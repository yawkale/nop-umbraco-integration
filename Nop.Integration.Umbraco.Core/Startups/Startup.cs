using System;
using System.Collections.Generic;
using System.Web;
using Nop.Integration.Umbraco.Core.Controllers;
using Nop.Integration.Umbraco.Core.Services;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;
using Umbraco.Core.Events;
using Nop.Integration.Umbraco.Category;
using System.Web.Configuration;
using Nop.Integration.Umbraco.Products;
using Umbraco.Web.Trees;
using Umbraco.Web.Models.Trees;

namespace Nop.Integration.Umbraco.Core
{
    public class Startup : IApplicationEventHandler
    {
        private readonly NopApiService _nopService;
        private readonly UserContext _userContext;
        //private const string CustomerId = "NopCustomerId";
        private const string PropertyTypeAlias = "nopCustomerId";
        private const string NopCategoryId = "nopCategoryId";
        private const string NopProductId = "nopProductId";
        protected const string NopDashboardSectionAlias = "nopdashboard";

        private readonly bool _autoCreateNopItem;
        private readonly string _categoriesAlias;
        private readonly string _productsAlias;

        public Startup()
        {
            _nopService = new NopApiService();
            _userContext = new UserContext();
            _autoCreateNopItem = bool.Parse(WebConfigurationManager.AppSettings["AutoCreateNopItem"]);
            _categoriesAlias = WebConfigurationManager.AppSettings["CategoriesContentTypeAlias"];
            _productsAlias = WebConfigurationManager.AppSettings["ProductsContentTypeAlias"];
        }

        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {

        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {

        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            MemberService.Saved += MemberService_Saved;
            ContentService.Saving += ContentService_Saved;
            TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;

            DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(DefaultController));

            var section = applicationContext.Services.SectionService.GetByAlias(NopDashboardSectionAlias);

            if (section == null)
            {
                applicationContext.Services.SectionService.MakeNew("NopDashboard", NopDashboardSectionAlias, "icon-newspaper");

            }


            //applicationContext.Services.UserService.AddSectionToAllUsers(DashboardAlias);
        }

        private void ContentService_Saved(IContentService sender, SaveEventArgs<IContent> e)
        {
            if (_autoCreateNopItem)
            {
                foreach (var entity in e.SavedEntities)
                {
                    if (entity.ContentType.Alias == "category")
                    {
                        if (string.IsNullOrEmpty(entity.GetValue<string>(NopCategoryId)))
                        {
                            var category = new PostCategoryObject()
                            {
                                Name = entity.Name
                            };

                            var categoryId = _nopService.CreateCategory(category);

                            entity.SetValue(NopCategoryId, categoryId);
                        }
                    }

                    if (entity.ContentType.Alias == "product")
                    {
                        if (string.IsNullOrEmpty(entity.GetValue<string>(NopProductId)))
                        {
                            var product = new PostProductObject()
                            {
                                Name = entity.Name
                            };

                            var productId = _nopService.CreateProduct(product);

                            entity.SetValue(NopProductId, productId);
                        }
                    }
                }
            }

        }

        private void MemberService_Saved(IMemberService sender, global::Umbraco.Core.Events.SaveEventArgs<IMember> e)
        {
            foreach (var member in e.SavedEntities)
            {
                if (string.IsNullOrEmpty(member.GetValue<string>(PropertyTypeAlias)))
                {
                    var customer = new Customer.Customer()
                    {
                        roles = new List<int>() { 3 },
                        FirstName = member.Name,
                        LastName = "",
                        Password = Guid.NewGuid().ToString(),
                        Email = member.Email
                    };

                    string customerId;

                    if (string.IsNullOrEmpty(_userContext.CustomerId()))
                    {
                        customerId = _nopService.CreateCustomer(customer);
                        _userContext.SetCustomerId(int.Parse( customerId));
                        
                    }
                    else
                    {
                        var nopCustomerId = _userContext.CustomerId();
                        customerId = _nopService.UpdateCustomer(customer, nopCustomerId);
                    }

                    member.SetValue(PropertyTypeAlias, customerId);
                }
            }
        }

        void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            
            if (sender.TreeAlias == "content" && e.NodeId!="-1")
            {
                var alias = sender.ApplicationContext.Services.ContentService.GetById(Int32.Parse(e.NodeId)).ContentType.Alias;

                if(alias == _categoriesAlias || alias == _productsAlias)
                {
                    var c = new MenuItem("createNopItem", "Create Nop Item");
                    c.LaunchDialogView("/App_Plugins/Global/views/createNopItem.html", "");
                    c.Icon = "umb-content";
                    e.Menu.Items.Add(c);

                    var u = new MenuItem("updateNopItem", "Update Nop Item");
                    u.LaunchDialogView("/App_Plugins/Global/views/updateNopItem.html", "");
                    u.Icon = "umb-content";
                    e.Menu.Items.Add(u);
                }
            }
        }
    }
}