using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Composing;


namespace Socius.PropertyEditors
{

	public class SociusFeedDataTypeComposer : ComponentComposer<SociusFeedDataTypeComponent>
	{
	}

	public class SociusFeedDataTypeComponent : IComponent
	{
		private readonly IDataTypeService _dataTypeService;
		private readonly IConfigurationEditorJsonSerializer _configurationEditorJsonSerializer;
		private readonly PropertyEditorCollection _propertyEditorCollection;

		public SociusFeedDataTypeComponent(
			IDataTypeService dataTypeService,
			IConfigurationEditorJsonSerializer configurationEditorJsonSerializer,
			PropertyEditorCollection propertyEditorCollection)
		{
			_dataTypeService = dataTypeService;
			_configurationEditorJsonSerializer = configurationEditorJsonSerializer;
			_propertyEditorCollection = propertyEditorCollection;
		}

		public void Initialize()
		{
			new SociusFeedDataType(_dataTypeService, _configurationEditorJsonSerializer, _propertyEditorCollection).Install();
		}

		public void Terminate()
		{
		}
	}


	public class SociusFeedDataType
	{
		private readonly IDataTypeService _dataTypeService;
		private readonly IConfigurationEditorJsonSerializer _configurationEditorJsonSerializer;
		private readonly PropertyEditorCollection _propertyEditorCollection;

		public SociusFeedDataType(
			IDataTypeService dataTypeService,
			IConfigurationEditorJsonSerializer configurationEditorJsonSerializer,
			PropertyEditorCollection propertyEditorCollection)
		{
			_dataTypeService = dataTypeService;
			_configurationEditorJsonSerializer = configurationEditorJsonSerializer;
			_propertyEditorCollection = propertyEditorCollection;
		}

		public void Install()
		{
			if (_dataTypeService.GetDataType(SociusConstants.Application.FeedDataTypeName) is not null)
			{
				return;
			}

			_propertyEditorCollection.TryGet(SociusConstants.Application.FeedPropertyEditorAlias, out IDataEditor? editor);
			_dataTypeService.Save(new DataType(editor, _configurationEditorJsonSerializer)
			{
				DatabaseType = ValueStorageType.Integer,
				CreateDate = DateTime.Now,
				CreatorId = -1,
				Name = SociusConstants.Application.FeedDataTypeName,
				Configuration = new SociusFeedConfiguration()
				{
					HideLabel = false,
					PostLimit = 1,
				}
			});
		}

	}
}
