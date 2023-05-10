using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Trees;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.ModelBinders;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Web.Common.Attributes;

namespace Socius.Dashboard
{
	[Tree(SociusConstants.Application.SectionAlias, treeAlias: SociusConstants.Application.TreeAlias, TreeTitle = SociusConstants.Application.TreeName, TreeGroup = Constants.Trees.Groups.ThirdParty, SortOrder = 12)]
	[PluginController(SociusConstants.PluginName)]
	public class SociusTreeController : TreeController
	{
		private readonly IMenuItemCollectionFactory _menuItemsFactory;

		public SociusTreeController(
			ILocalizedTextService localizedTextService,
			UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection,
			IEventAggregator eventAggregator,
			IMenuItemCollectionFactory menuItemCollectionFactory) : base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
		{
			_menuItemsFactory = menuItemCollectionFactory;
		}

		protected override ActionResult<TreeNode?> CreateRootNode(FormCollection queryStrings)
		{
			var rootResult = base.CreateRootNode(queryStrings);

			if (rootResult.Result is not null)
			{
				return rootResult;
			}

			var root = rootResult.Value;

			if (root is not null)
			{
				root.RoutePath = SociusConstants.Urls.WelcomeUrl;
				root.Icon = "icon-share";
				root.HasChildren = true;
				root.MenuUrl = null;
			}

			return root;
		}

		protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
		{
			if (id != Constants.System.RootString)
			{
				throw new InvalidOperationException("Id not Root");
			}

			var tree = new TreeNodeCollection();

			if (id == Constants.System.RootString)
			{
				tree.AddRange(PopulateTreeNodes(id, queryStrings));
			}

			return tree;
		}

		protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
		{
			var menu = _menuItemsFactory.Create();

			if (id == Constants.System.Root.ToInvariantString())
			{
				menu.Items.Add(new RefreshNode(this.LocalizedTextService, true));
			}

			return menu;
		}

		private TreeNodeCollection PopulateTreeNodes(string parentId, FormCollection qs)
		{
			return new TreeNodeCollection
			{
				CreateTreeNode(
					SociusConstants.Application.ProfilesDashboardAlias,
					parentId,
					qs,
					SociusConstants.Application.ProfilesDashboardName,
					"icon-share",
					false,
					SociusConstants.Urls.ProfilesUrl),

				CreateTreeNode(
					SociusConstants.Application.InteractionDashboardAlias,
					parentId,
					qs,
					SociusConstants.Application.InteractionDashboardName,
					"icon-hand-pointer",
					false,
					SociusConstants.Urls.InteractionUrl),
			};
		}
	}
}
