using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator.Internals
{
    public class StringBuilderPlus
    {

        #region Private Fields

        private readonly bool allowIndentation;

        private readonly string padding = @"  ";

        private readonly StringBuilder stringBuilder = new StringBuilder();

        private bool isContinuingFromPrevious = false;

        #endregion Private Fields

        #region Public Constructors

        public StringBuilderPlus(bool allowIndentation, int initialLevel)
        {
            this.allowIndentation = allowIndentation;
            this.Level = initialLevel;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Level { get; private set; }

        public string Text { get { return this.stringBuilder.ToString(); } }

        #endregion Public Properties

        #region Public Methods

        public void Append(string value)
        {
            if (!isContinuingFromPrevious)
            {
                AddPadding();
            }
            this.stringBuilder.Append(value);
            isContinuingFromPrevious = true;
        }

        public void AppendLine()
        {
            if (allowIndentation)
            {
                this.AppendLine(string.Empty);
            }
            isContinuingFromPrevious = false;
        }

        public void AppendLine(string value)
        {
            if (!isContinuingFromPrevious)
            {
                AddPadding();
            }
            if (allowIndentation)
            {
                this.stringBuilder.AppendLine(value);
            }
            else
            {
                this.stringBuilder.Append(value);
            }
            isContinuingFromPrevious = false;
        }

        public IDisposable BeginIndent()
        {
            return new IndentLevel(this);
        }

        public IDisposable BeginNoIndent()
        {
            return new NoIndentLevel(this);
        }

        public override string ToString()
        {
            return this.stringBuilder.ToString();
        }

        #endregion Public Methods

        #region Private Methods

        private void AddPadding()
        {
            if (this.allowIndentation && Level > 0)
            {
                for (var i = 0; i < Level; i++)
                {
                    this.stringBuilder.Append(padding);
                }
            }
        }

        #endregion Private Methods

        #region Private Classes

        private class IndentLevel : IDisposable
        {

            #region Private Fields

            private readonly StringBuilderPlus stringBuilderPlus;

            #endregion Private Fields

            #region Public Constructors

            public IndentLevel(StringBuilderPlus stringBuilderPlus)
            {
                this.stringBuilderPlus = stringBuilderPlus;
                if (this.stringBuilderPlus.Level < 0)
                {
                    this.stringBuilderPlus.Level = 0;
                }
                else
                {
                    this.stringBuilderPlus.Level++;
                }
            }

            #endregion Public Constructors

            #region Public Methods

            public void Dispose()
            {
                if (this.stringBuilderPlus.Level > 0)
                {
                    this.stringBuilderPlus.Level--;
                }
            }

            #endregion Public Methods

        }

        private class NoIndentLevel : IDisposable
        {

            #region Private Fields

            private readonly StringBuilderPlus stringBuilderPlus;

            #endregion Private Fields

            #region Public Constructors

            public NoIndentLevel(StringBuilderPlus stringBuilderPlus)
            {
                this.stringBuilderPlus = stringBuilderPlus;

            }

            #endregion Public Constructors

            #region Public Methods

            public void Dispose()
            {

            }

            #endregion Public Methods

        }

        #endregion Private Classes
    }
}
