using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.Validators;
using Umbraco.Cms.Core.Services;

namespace Socius.PropertyEditors
{

	[DataEditor(
		SociusConstants.Application.FeedPropertyEditorAlias, 
		EditorType.PropertyValue,
		SociusConstants.Application.FeedPropertyEditorName,
		"/App_Plugins/Socius/PropertyEditors/SociusFeedEditor.html", 
		ValueType = ValueTypes.Integer, 
		Icon = "icon-share-alt-2",
		ValueEditorIsReusable = true)]
	public class SociusFeedEditor : DataEditor
	{
		private readonly IIOHelper _ioHelper;
		private readonly IEditorConfigurationParser _configParser;

		public SociusFeedEditor(
			IDataValueEditorFactory dataValueEditorFactory, 
			IIOHelper ioHelper,
			IEditorConfigurationParser configParser,
			EditorType type = EditorType.PropertyValue) 
			: base(dataValueEditorFactory, type)
		{
			_ioHelper = ioHelper;
			_configParser = configParser;
			SupportsReadOnly = true;
		}


		protected override IConfigurationEditor CreateConfigurationEditor() => new SociusFeedConfigurationEditor(_ioHelper, _configParser);

		protected override IDataValueEditor CreateValueEditor()
		{
			IDataValueEditor editor = base.CreateValueEditor();
			editor.Validators.Add(new IntegerValidator());
			return editor;
		}

		public override IDataValueEditor GetValueEditor(object? configuration)
		{
			var editor = base.GetValueEditor(configuration);

			if (editor is DataValueEditor valueEditor && configuration is SociusFeedConfiguration config)
			{
				valueEditor.HideLabel = config.HideLabel;
			}

			return editor;
		}

	}


	public class SociusFeedConfigurationEditor : ConfigurationEditor<SociusFeedConfiguration>
	{
		public SociusFeedConfigurationEditor(
			IIOHelper ioHelper, 
			IEditorConfigurationParser editorConfigurationParser) 
			: base(ioHelper, editorConfigurationParser)
		{
		}
	}


	public class SociusFeedConfiguration
	{
		[ConfigurationField("hideLabel", "Hide Label?", "boolean", Description = "Hide the property label.")]
		public bool HideLabel { get; set; }

		[ConfigurationField("postLimit", "Number of posts per feed", "requiredfield", Description = "The number of posts retrieved per feed.")]
		public int PostLimit { get; set; } = 1;
	}
}