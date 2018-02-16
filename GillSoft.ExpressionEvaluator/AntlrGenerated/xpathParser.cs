//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ..\Grammar\xpath.g4 by ANTLR 4.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace GillSoft.ExpressionEvaluator {
using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6")]
[System.CLSCompliant(false)]
public partial class xpathParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		AXIS=1, IDENT=2, PATHSEP=3, LBRAC=4, RBRAC=5, AT=6, EQ=7, COLON=8, COLONCOLON=9, 
		STRING=10, Whitespace=11;
	public const int
		RULE_path = 0, RULE_pathElement = 1, RULE_filter = 2, RULE_axis = 3, RULE_namespacePrefix = 4, 
		RULE_attribute = 5, RULE_element = 6;
	public static readonly string[] ruleNames = {
		"path", "pathElement", "filter", "axis", "namespacePrefix", "attribute", 
		"element"
	};

	private static readonly string[] _LiteralNames = {
		null, null, null, "'/'", "'['", "']'", "'@'", null, "':'", "'::'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "AXIS", "IDENT", "PATHSEP", "LBRAC", "RBRAC", "AT", "EQ", "COLON", 
		"COLONCOLON", "STRING", "Whitespace"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "xpath.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	static xpathParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

	public xpathParser(ITokenStream input)
		: base(input)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}
	public partial class PathContext : ParserRuleContext {
		public PathElementContext documentRoot;
		public PathElementContext documentChildren;
		public ITerminalNode[] PATHSEP() { return GetTokens(xpathParser.PATHSEP); }
		public ITerminalNode PATHSEP(int i) {
			return GetToken(xpathParser.PATHSEP, i);
		}
		public PathElementContext[] pathElement() {
			return GetRuleContexts<PathElementContext>();
		}
		public PathElementContext pathElement(int i) {
			return GetRuleContext<PathElementContext>(i);
		}
		public PathContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_path; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IxpathVisitor<TResult> typedVisitor = visitor as IxpathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPath(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public PathContext path() {
		PathContext _localctx = new PathContext(Context, State);
		EnterRule(_localctx, 0, RULE_path);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 14; Match(PATHSEP);
			State = 15; _localctx.documentRoot = pathElement();
			State = 20;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==PATHSEP) {
				{
				{
				State = 16; Match(PATHSEP);
				State = 17; _localctx.documentChildren = pathElement();
				}
				}
				State = 22;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class PathElementContext : ParserRuleContext {
		public ElementContext element() {
			return GetRuleContext<ElementContext>(0);
		}
		public AxisContext axis() {
			return GetRuleContext<AxisContext>(0);
		}
		public FilterContext filter() {
			return GetRuleContext<FilterContext>(0);
		}
		public AttributeContext attribute() {
			return GetRuleContext<AttributeContext>(0);
		}
		public PathElementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_pathElement; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IxpathVisitor<TResult> typedVisitor = visitor as IxpathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPathElement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public PathElementContext pathElement() {
		PathElementContext _localctx = new PathElementContext(Context, State);
		EnterRule(_localctx, 2, RULE_pathElement);
		int _la;
		try {
			State = 31;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case AXIS:
			case IDENT:
				EnterOuterAlt(_localctx, 1);
				{
				{
				State = 24;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				if (_la==AXIS) {
					{
					State = 23; axis();
					}
				}

				State = 26; element();
				State = 28;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				if (_la==LBRAC) {
					{
					State = 27; filter();
					}
				}

				}
				}
				break;
			case AT:
				EnterOuterAlt(_localctx, 2);
				{
				State = 30; attribute();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class FilterContext : ParserRuleContext {
		public AttributeContext attr;
		public ElementContext elem;
		public IToken value;
		public ITerminalNode LBRAC() { return GetToken(xpathParser.LBRAC, 0); }
		public ITerminalNode EQ() { return GetToken(xpathParser.EQ, 0); }
		public ITerminalNode RBRAC() { return GetToken(xpathParser.RBRAC, 0); }
		public ITerminalNode STRING() { return GetToken(xpathParser.STRING, 0); }
		public AttributeContext attribute() {
			return GetRuleContext<AttributeContext>(0);
		}
		public ElementContext element() {
			return GetRuleContext<ElementContext>(0);
		}
		public FilterContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_filter; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IxpathVisitor<TResult> typedVisitor = visitor as IxpathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFilter(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FilterContext filter() {
		FilterContext _localctx = new FilterContext(Context, State);
		EnterRule(_localctx, 4, RULE_filter);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 33; Match(LBRAC);
			State = 36;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case AT:
				{
				State = 34; _localctx.attr = attribute();
				}
				break;
			case IDENT:
				{
				State = 35; _localctx.elem = element();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			State = 38; Match(EQ);
			State = 39; _localctx.value = Match(STRING);
			State = 40; Match(RBRAC);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AxisContext : ParserRuleContext {
		public IToken name;
		public ITerminalNode COLONCOLON() { return GetToken(xpathParser.COLONCOLON, 0); }
		public ITerminalNode AXIS() { return GetToken(xpathParser.AXIS, 0); }
		public AxisContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_axis; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IxpathVisitor<TResult> typedVisitor = visitor as IxpathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAxis(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AxisContext axis() {
		AxisContext _localctx = new AxisContext(Context, State);
		EnterRule(_localctx, 6, RULE_axis);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 42; _localctx.name = Match(AXIS);
			State = 43; Match(COLONCOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NamespacePrefixContext : ParserRuleContext {
		public IToken name;
		public ITerminalNode IDENT() { return GetToken(xpathParser.IDENT, 0); }
		public NamespacePrefixContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_namespacePrefix; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IxpathVisitor<TResult> typedVisitor = visitor as IxpathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNamespacePrefix(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public NamespacePrefixContext namespacePrefix() {
		NamespacePrefixContext _localctx = new NamespacePrefixContext(Context, State);
		EnterRule(_localctx, 8, RULE_namespacePrefix);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 45; _localctx.name = Match(IDENT);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AttributeContext : ParserRuleContext {
		public NamespacePrefixContext ns;
		public IToken name;
		public ITerminalNode AT() { return GetToken(xpathParser.AT, 0); }
		public ITerminalNode IDENT() { return GetToken(xpathParser.IDENT, 0); }
		public ITerminalNode COLON() { return GetToken(xpathParser.COLON, 0); }
		public NamespacePrefixContext namespacePrefix() {
			return GetRuleContext<NamespacePrefixContext>(0);
		}
		public AttributeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_attribute; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IxpathVisitor<TResult> typedVisitor = visitor as IxpathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAttribute(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AttributeContext attribute() {
		AttributeContext _localctx = new AttributeContext(Context, State);
		EnterRule(_localctx, 10, RULE_attribute);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 47; Match(AT);
			State = 51;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,5,Context) ) {
			case 1:
				{
				State = 48; _localctx.ns = namespacePrefix();
				State = 49; Match(COLON);
				}
				break;
			}
			State = 53; _localctx.name = Match(IDENT);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ElementContext : ParserRuleContext {
		public NamespacePrefixContext ns;
		public IToken name;
		public ITerminalNode IDENT() { return GetToken(xpathParser.IDENT, 0); }
		public ITerminalNode COLON() { return GetToken(xpathParser.COLON, 0); }
		public NamespacePrefixContext namespacePrefix() {
			return GetRuleContext<NamespacePrefixContext>(0);
		}
		public ElementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_element; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IxpathVisitor<TResult> typedVisitor = visitor as IxpathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitElement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ElementContext element() {
		ElementContext _localctx = new ElementContext(Context, State);
		EnterRule(_localctx, 12, RULE_element);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 58;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,6,Context) ) {
			case 1:
				{
				State = 55; _localctx.ns = namespacePrefix();
				State = 56; Match(COLON);
				}
				break;
			}
			State = 60; _localctx.name = Match(IDENT);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static string _serializedATN = _serializeATN();
	private static string _serializeATN()
	{
	    StringBuilder sb = new StringBuilder();
	    sb.Append("\x3\x430\xD6D1\x8206\xAD2D\x4417\xAEF1\x8D80\xAADD\x3\r\x41");
		sb.Append("\x4\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a");
		sb.Append("\t\a\x4\b\t\b\x3\x2\x3\x2\x3\x2\x3\x2\a\x2\x15\n\x2\f\x2\xE");
		sb.Append("\x2\x18\v\x2\x3\x3\x5\x3\x1B\n\x3\x3\x3\x3\x3\x5\x3\x1F\n\x3");
		sb.Append("\x3\x3\x5\x3\"\n\x3\x3\x4\x3\x4\x3\x4\x5\x4\'\n\x4\x3\x4\x3");
		sb.Append("\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\a\x3\a\x3\a");
		sb.Append("\x3\a\x5\a\x36\n\a\x3\a\x3\a\x3\b\x3\b\x3\b\x5\b=\n\b\x3\b\x3");
		sb.Append("\b\x3\b\x2\x2\t\x2\x4\x6\b\n\f\xE\x2\x2@\x2\x10\x3\x2\x2\x2");
		sb.Append("\x4!\x3\x2\x2\x2\x6#\x3\x2\x2\x2\b,\x3\x2\x2\x2\n/\x3\x2\x2");
		sb.Append("\x2\f\x31\x3\x2\x2\x2\xE<\x3\x2\x2\x2\x10\x11\a\x5\x2\x2\x11");
		sb.Append("\x16\x5\x4\x3\x2\x12\x13\a\x5\x2\x2\x13\x15\x5\x4\x3\x2\x14");
		sb.Append("\x12\x3\x2\x2\x2\x15\x18\x3\x2\x2\x2\x16\x14\x3\x2\x2\x2\x16");
		sb.Append("\x17\x3\x2\x2\x2\x17\x3\x3\x2\x2\x2\x18\x16\x3\x2\x2\x2\x19");
		sb.Append("\x1B\x5\b\x5\x2\x1A\x19\x3\x2\x2\x2\x1A\x1B\x3\x2\x2\x2\x1B");
		sb.Append("\x1C\x3\x2\x2\x2\x1C\x1E\x5\xE\b\x2\x1D\x1F\x5\x6\x4\x2\x1E");
		sb.Append("\x1D\x3\x2\x2\x2\x1E\x1F\x3\x2\x2\x2\x1F\"\x3\x2\x2\x2 \"\x5");
		sb.Append("\f\a\x2!\x1A\x3\x2\x2\x2! \x3\x2\x2\x2\"\x5\x3\x2\x2\x2#&\a");
		sb.Append("\x6\x2\x2$\'\x5\f\a\x2%\'\x5\xE\b\x2&$\x3\x2\x2\x2&%\x3\x2\x2");
		sb.Append("\x2\'(\x3\x2\x2\x2()\a\t\x2\x2)*\a\f\x2\x2*+\a\a\x2\x2+\a\x3");
		sb.Append("\x2\x2\x2,-\a\x3\x2\x2-.\a\v\x2\x2.\t\x3\x2\x2\x2/\x30\a\x4");
		sb.Append("\x2\x2\x30\v\x3\x2\x2\x2\x31\x35\a\b\x2\x2\x32\x33\x5\n\x6\x2");
		sb.Append("\x33\x34\a\n\x2\x2\x34\x36\x3\x2\x2\x2\x35\x32\x3\x2\x2\x2\x35");
		sb.Append("\x36\x3\x2\x2\x2\x36\x37\x3\x2\x2\x2\x37\x38\a\x4\x2\x2\x38");
		sb.Append("\r\x3\x2\x2\x2\x39:\x5\n\x6\x2:;\a\n\x2\x2;=\x3\x2\x2\x2<\x39");
		sb.Append("\x3\x2\x2\x2<=\x3\x2\x2\x2=>\x3\x2\x2\x2>?\a\x4\x2\x2?\xF\x3");
		sb.Append("\x2\x2\x2\t\x16\x1A\x1E!&\x35<");
	    return sb.ToString();
	}

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());


}
} // namespace GillSoft.ExpressionEvaluator
