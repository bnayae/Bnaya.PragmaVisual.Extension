using System.ComponentModel.Composition;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Bnaya.PragmaVisual.Extension
{
    /// <summary>
    /// Defines an editor format for the PragmaClassifier type
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "PragmaClassifier")]
    [Name("PragmaClassifier")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.Default)] // Set the priority to be after the default classifiers
    internal sealed class PragmaClassifierFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PragmaClassifierFormat"/> class.
        /// </summary>
        public PragmaClassifierFormat()
        {
            this.DisplayName = "Pragma-Classifier"; // Human readable version of the name
            this.ForegroundOpacity = 0.15;
            if (this.FontRenderingSize != null)
                this.FontRenderingSize /= 2;
            else if (this.FontHintingSize != null)
                this.FontHintingSize /= 2;
            else
                this.FontRenderingSize = 10;
        }
    }
}
