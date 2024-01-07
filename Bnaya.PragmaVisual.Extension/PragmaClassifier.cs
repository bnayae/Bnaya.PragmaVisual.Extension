using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Bnaya.PragmaVisual.Extension
{
    /// <summary>
    /// Classifier that classifies all text as an instance of the "PragmaClassifier" classification type.
    /// </summary>
    internal class PragmaClassifier : IClassifier
    {
        private static readonly IList<ClassificationSpan> EMPTY = new List<ClassificationSpan>();

        private int _i = 0;

        /// <summary>
        /// Classification type.
        /// </summary>
        private readonly IClassificationType classificationType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PragmaClassifier"/> class.
        /// </summary>
        /// <param name="registry">Classification registry.</param>
        internal PragmaClassifier(IClassificationTypeRegistryService registry)
        {
            this.classificationType = registry.GetClassificationType("PragmaClassifier");
        }

        #region IClassifier

#pragma warning disable 67

        /// <summary>
        /// An event that occurs when the classification of a span of text has changed.
        /// </summary>
        /// <remarks>
        /// This event gets raised if a non-text change would affect the classification in some way,
        /// for example typing /* would cause the classification to change in C# without directly
        /// affecting the span.
        /// </remarks>
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        /// <summary>
        /// Gets all the <see cref="ClassificationSpan"/> objects that intersect with the given range of text.
        /// </summary>
        /// <remarks>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </remarks>
        /// <param name="span">The span currently being classified.</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            if(span.IsEmpty)
                return EMPTY;
            int len = span.End.Position - span.Start.Position;
            if (len < 7) 
                return EMPTY;

            string prefix = span.Snapshot.GetText(span.Start.Position, 7);
            if (prefix != "#pragma")
            {
                var res = EMPTY;
                return res;
            }

            var result = new List<ClassificationSpan>()
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), this.classificationType)
            };

            return result;
        }

        #endregion
    }
}
