using BlazorMonaco.Editor;
using Microsoft.AspNetCore.Components;

namespace LF2IDE.BlazorServer.Components
{
	public partial class CodeEditor
	{
		[Parameter] public string? FilePath { get; set; }

		private StandaloneCodeEditor? _editor;
		private bool _isInitialized = false;

		private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
		{
			return new StandaloneEditorConstructionOptions
			{
				Language = "plaintext",
				Value = EditorState.CurrentLF2DataFile?.Content ?? "",
				Theme = "vs",
				AutomaticLayout = true,
				FontSize = 14,
				LineNumbers = "on",
				RenderWhitespace = "selection",
				ScrollBeyondLastLine = false
			};
		}

		private async Task EditorOnDidInit()
		{
			_isInitialized = true;

			if (_editor != null && EditorState.CurrentLF2DataFile != null)
			{
				await _editor.SetValue(EditorState.CurrentLF2DataFile.Content);
			}
		}

		private async Task OnContentChanged(ModelContentChangedEvent e)
		{
			if (_editor != null && EditorState.CurrentLF2DataFile != null && _isInitialized)
			{
				var content = await _editor.GetValue();
				EditorState.CurrentLF2DataFile.Content = content;
				EditorState.CurrentLF2DataFile.IsModified = true;
			}
		}

		protected override async Task OnParametersSetAsync()
		{
			if (_isInitialized && _editor != null && EditorState.CurrentLF2DataFile != null)
			{
				await _editor.SetValue(EditorState.CurrentLF2DataFile.Content);
			}
		}
	}
}
