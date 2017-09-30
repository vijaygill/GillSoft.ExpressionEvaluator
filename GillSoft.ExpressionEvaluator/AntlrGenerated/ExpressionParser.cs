//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ..\Grammar\Expression.g4 by ANTLR 4.6

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
public partial class ExpressionParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, STRING=3, SINGLE_QUOTED_STRING=4, DOUBLE_QUOTED_STRING=5, 
		CONST=6, INTEGER=7, DECIMAL=8, TRUE=9, FALSE=10, IDENT=11, LPAREN=12, 
		RPAREN=13, MULT=14, DIV=15, ADD=16, SUB=17, POW=18, AND=19, OR=20, WS=21;
	public const int
		RULE_expression = 0, RULE_mathematicalExpression = 1, RULE_booleanExprerssion = 2, 
		RULE_subExpression = 3, RULE_function = 4, RULE_simpleValue = 5;
	public static readonly string[] ruleNames = {
		"expression", "mathematicalExpression", "booleanExprerssion", "subExpression", 
		"function", "simpleValue"
	};

	private static readonly string[] _LiteralNames = {
		null, "'!'", "','", null, null, null, null, null, null, null, null, null, 
		"'('", "')'", "'*'", "'/'", "'+'", "'-'", "'^'", "'&&'", "'||'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, "STRING", "SINGLE_QUOTED_STRING", "DOUBLE_QUOTED_STRING", 
		"CONST", "INTEGER", "DECIMAL", "TRUE", "FALSE", "IDENT", "LPAREN", "RPAREN", 
		"MULT", "DIV", "ADD", "SUB", "POW", "AND", "OR", "WS"
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

	public override string GrammarFileName { get { return "Expression.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	static ExpressionParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

	public ExpressionParser(ITokenStream input)
		: base(input)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}
	public partial class ExpressionContext : ParserRuleContext {
		public MathematicalExpressionContext mathematicalExpression() {
			return GetRuleContext<MathematicalExpressionContext>(0);
		}
		public BooleanExprerssionContext booleanExprerssion() {
			return GetRuleContext<BooleanExprerssionContext>(0);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expression; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IExpressionVisitor<TResult> typedVisitor = visitor as IExpressionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExpressionContext expression() {
		ExpressionContext _localctx = new ExpressionContext(Context, State);
		EnterRule(_localctx, 0, RULE_expression);
		try {
			State = 14;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,0,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 12; mathematicalExpression(0);
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 13; booleanExprerssion(0);
				}
				break;
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

	public partial class MathematicalExpressionContext : ParserRuleContext {
		public MathematicalExpressionContext left;
		public IToken sign;
		public MathematicalExpressionContext expr;
		public FunctionContext functionValue;
		public SimpleValueContext value;
		public IToken op;
		public MathematicalExpressionContext right;
		public MathematicalExpressionContext[] mathematicalExpression() {
			return GetRuleContexts<MathematicalExpressionContext>();
		}
		public MathematicalExpressionContext mathematicalExpression(int i) {
			return GetRuleContext<MathematicalExpressionContext>(i);
		}
		public SubExpressionContext subExpression() {
			return GetRuleContext<SubExpressionContext>(0);
		}
		public FunctionContext function() {
			return GetRuleContext<FunctionContext>(0);
		}
		public SimpleValueContext simpleValue() {
			return GetRuleContext<SimpleValueContext>(0);
		}
		public ITerminalNode POW() { return GetToken(ExpressionParser.POW, 0); }
		public ITerminalNode MULT() { return GetToken(ExpressionParser.MULT, 0); }
		public ITerminalNode DIV() { return GetToken(ExpressionParser.DIV, 0); }
		public ITerminalNode ADD() { return GetToken(ExpressionParser.ADD, 0); }
		public ITerminalNode SUB() { return GetToken(ExpressionParser.SUB, 0); }
		public MathematicalExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_mathematicalExpression; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IExpressionVisitor<TResult> typedVisitor = visitor as IExpressionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMathematicalExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MathematicalExpressionContext mathematicalExpression() {
		return mathematicalExpression(0);
	}

	private MathematicalExpressionContext mathematicalExpression(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		MathematicalExpressionContext _localctx = new MathematicalExpressionContext(Context, _parentState);
		MathematicalExpressionContext _prevctx = _localctx;
		int _startState = 2;
		EnterRecursionRule(_localctx, 2, RULE_mathematicalExpression, _p);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 22;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,1,Context) ) {
			case 1:
				{
				State = 17;
				_localctx.sign = TokenStream.LT(1);
				_la = TokenStream.LA(1);
				if ( !(_la==ADD || _la==SUB) ) {
					_localctx.sign = ErrorHandler.RecoverInline(this);
				}
				else {
					ErrorHandler.ReportMatch(this);
				    Consume();
				}
				State = 18; _localctx.expr = mathematicalExpression(7);
				}
				break;
			case 2:
				{
				State = 19; subExpression();
				}
				break;
			case 3:
				{
				State = 20; _localctx.functionValue = function();
				}
				break;
			case 4:
				{
				State = 21; _localctx.value = simpleValue();
				}
				break;
			}
			Context.Stop = TokenStream.LT(-1);
			State = 35;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,3,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 33;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,2,Context) ) {
					case 1:
						{
						_localctx = new MathematicalExpressionContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_mathematicalExpression);
						State = 24;
						if (!(Precpred(Context, 5))) throw new FailedPredicateException(this, "Precpred(Context, 5)");
						State = 25; _localctx.op = Match(POW);
						State = 26; _localctx.right = mathematicalExpression(6);
						}
						break;
					case 2:
						{
						_localctx = new MathematicalExpressionContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_mathematicalExpression);
						State = 27;
						if (!(Precpred(Context, 4))) throw new FailedPredicateException(this, "Precpred(Context, 4)");
						State = 28;
						_localctx.op = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==MULT || _la==DIV) ) {
							_localctx.op = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 29; _localctx.right = mathematicalExpression(5);
						}
						break;
					case 3:
						{
						_localctx = new MathematicalExpressionContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_mathematicalExpression);
						State = 30;
						if (!(Precpred(Context, 3))) throw new FailedPredicateException(this, "Precpred(Context, 3)");
						State = 31;
						_localctx.op = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==ADD || _la==SUB) ) {
							_localctx.op = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 32; _localctx.right = mathematicalExpression(4);
						}
						break;
					}
					} 
				}
				State = 37;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,3,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public partial class BooleanExprerssionContext : ParserRuleContext {
		public BooleanExprerssionContext left;
		public IToken sign;
		public BooleanExprerssionContext expr;
		public FunctionContext functionValue;
		public SimpleValueContext value;
		public IToken op;
		public BooleanExprerssionContext right;
		public BooleanExprerssionContext[] booleanExprerssion() {
			return GetRuleContexts<BooleanExprerssionContext>();
		}
		public BooleanExprerssionContext booleanExprerssion(int i) {
			return GetRuleContext<BooleanExprerssionContext>(i);
		}
		public SubExpressionContext subExpression() {
			return GetRuleContext<SubExpressionContext>(0);
		}
		public FunctionContext function() {
			return GetRuleContext<FunctionContext>(0);
		}
		public SimpleValueContext simpleValue() {
			return GetRuleContext<SimpleValueContext>(0);
		}
		public ITerminalNode AND() { return GetToken(ExpressionParser.AND, 0); }
		public ITerminalNode OR() { return GetToken(ExpressionParser.OR, 0); }
		public BooleanExprerssionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_booleanExprerssion; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IExpressionVisitor<TResult> typedVisitor = visitor as IExpressionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitBooleanExprerssion(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public BooleanExprerssionContext booleanExprerssion() {
		return booleanExprerssion(0);
	}

	private BooleanExprerssionContext booleanExprerssion(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		BooleanExprerssionContext _localctx = new BooleanExprerssionContext(Context, _parentState);
		BooleanExprerssionContext _prevctx = _localctx;
		int _startState = 4;
		EnterRecursionRule(_localctx, 4, RULE_booleanExprerssion, _p);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 44;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,4,Context) ) {
			case 1:
				{
				State = 39; _localctx.sign = Match(T__0);
				State = 40; _localctx.expr = booleanExprerssion(6);
				}
				break;
			case 2:
				{
				State = 41; subExpression();
				}
				break;
			case 3:
				{
				State = 42; _localctx.functionValue = function();
				}
				break;
			case 4:
				{
				State = 43; _localctx.value = simpleValue();
				}
				break;
			}
			Context.Stop = TokenStream.LT(-1);
			State = 54;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,6,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 52;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,5,Context) ) {
					case 1:
						{
						_localctx = new BooleanExprerssionContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_booleanExprerssion);
						State = 46;
						if (!(Precpred(Context, 4))) throw new FailedPredicateException(this, "Precpred(Context, 4)");
						State = 47; _localctx.op = Match(AND);
						State = 48; _localctx.right = booleanExprerssion(5);
						}
						break;
					case 2:
						{
						_localctx = new BooleanExprerssionContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_booleanExprerssion);
						State = 49;
						if (!(Precpred(Context, 3))) throw new FailedPredicateException(this, "Precpred(Context, 3)");
						State = 50; _localctx.op = Match(OR);
						State = 51; _localctx.right = booleanExprerssion(4);
						}
						break;
					}
					} 
				}
				State = 56;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,6,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public partial class SubExpressionContext : ParserRuleContext {
		public MathematicalExpressionContext mathExpr;
		public BooleanExprerssionContext boolExpr;
		public ITerminalNode LPAREN() { return GetToken(ExpressionParser.LPAREN, 0); }
		public ITerminalNode RPAREN() { return GetToken(ExpressionParser.RPAREN, 0); }
		public MathematicalExpressionContext mathematicalExpression() {
			return GetRuleContext<MathematicalExpressionContext>(0);
		}
		public BooleanExprerssionContext booleanExprerssion() {
			return GetRuleContext<BooleanExprerssionContext>(0);
		}
		public SubExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_subExpression; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IExpressionVisitor<TResult> typedVisitor = visitor as IExpressionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSubExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SubExpressionContext subExpression() {
		SubExpressionContext _localctx = new SubExpressionContext(Context, State);
		EnterRule(_localctx, 6, RULE_subExpression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 57; Match(LPAREN);
			State = 60;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,7,Context) ) {
			case 1:
				{
				State = 58; _localctx.mathExpr = mathematicalExpression(0);
				}
				break;
			case 2:
				{
				State = 59; _localctx.boolExpr = booleanExprerssion(0);
				}
				break;
			}
			State = 62; Match(RPAREN);
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

	public partial class FunctionContext : ParserRuleContext {
		public IToken name;
		public ExpressionContext paramFirst;
		public ExpressionContext _expression;
		public IList<ExpressionContext> _paramRest = new List<ExpressionContext>();
		public ITerminalNode LPAREN() { return GetToken(ExpressionParser.LPAREN, 0); }
		public ITerminalNode RPAREN() { return GetToken(ExpressionParser.RPAREN, 0); }
		public ITerminalNode IDENT() { return GetToken(ExpressionParser.IDENT, 0); }
		public ExpressionContext[] expression() {
			return GetRuleContexts<ExpressionContext>();
		}
		public ExpressionContext expression(int i) {
			return GetRuleContext<ExpressionContext>(i);
		}
		public FunctionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_function; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IExpressionVisitor<TResult> typedVisitor = visitor as IExpressionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFunction(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FunctionContext function() {
		FunctionContext _localctx = new FunctionContext(Context, State);
		EnterRule(_localctx, 8, RULE_function);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 64; _localctx.name = Match(IDENT);
			State = 65; Match(LPAREN);
			State = 74;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << STRING) | (1L << CONST) | (1L << TRUE) | (1L << FALSE) | (1L << IDENT) | (1L << LPAREN) | (1L << ADD) | (1L << SUB))) != 0)) {
				{
				State = 66; _localctx.paramFirst = expression();
				State = 71;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				while (_la==T__1) {
					{
					{
					State = 67; Match(T__1);
					State = 68; _localctx._expression = expression();
					_localctx._paramRest.Add(_localctx._expression);
					}
					}
					State = 73;
					ErrorHandler.Sync(this);
					_la = TokenStream.LA(1);
				}
				}
			}

			State = 76; Match(RPAREN);
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

	public partial class SimpleValueContext : ParserRuleContext {
		public IToken value;
		public ITerminalNode IDENT() { return GetToken(ExpressionParser.IDENT, 0); }
		public ITerminalNode CONST() { return GetToken(ExpressionParser.CONST, 0); }
		public ITerminalNode TRUE() { return GetToken(ExpressionParser.TRUE, 0); }
		public ITerminalNode FALSE() { return GetToken(ExpressionParser.FALSE, 0); }
		public ITerminalNode STRING() { return GetToken(ExpressionParser.STRING, 0); }
		public SimpleValueContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_simpleValue; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IExpressionVisitor<TResult> typedVisitor = visitor as IExpressionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSimpleValue(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SimpleValueContext simpleValue() {
		SimpleValueContext _localctx = new SimpleValueContext(Context, State);
		EnterRule(_localctx, 10, RULE_simpleValue);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 78;
			_localctx.value = TokenStream.LT(1);
			_la = TokenStream.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << STRING) | (1L << CONST) | (1L << TRUE) | (1L << FALSE) | (1L << IDENT))) != 0)) ) {
				_localctx.value = ErrorHandler.RecoverInline(this);
			}
			else {
				ErrorHandler.ReportMatch(this);
			    Consume();
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

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1: return mathematicalExpression_sempred((MathematicalExpressionContext)_localctx, predIndex);
		case 2: return booleanExprerssion_sempred((BooleanExprerssionContext)_localctx, predIndex);
		}
		return true;
	}
	private bool mathematicalExpression_sempred(MathematicalExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 5);
		case 1: return Precpred(Context, 4);
		case 2: return Precpred(Context, 3);
		}
		return true;
	}
	private bool booleanExprerssion_sempred(BooleanExprerssionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 3: return Precpred(Context, 4);
		case 4: return Precpred(Context, 3);
		}
		return true;
	}

	private static string _serializedATN = _serializeATN();
	private static string _serializeATN()
	{
	    StringBuilder sb = new StringBuilder();
	    sb.Append("\x3\x430\xD6D1\x8206\xAD2D\x4417\xAEF1\x8D80\xAADD\x3\x17");
		sb.Append("S\x4\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4");
		sb.Append("\a\t\a\x3\x2\x3\x2\x5\x2\x11\n\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3");
		sb.Append("\x3\x3\x3\x5\x3\x19\n\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3");
		sb.Append("\x3\x3\x3\x3\x3\x3\a\x3$\n\x3\f\x3\xE\x3\'\v\x3\x3\x4\x3\x4");
		sb.Append("\x3\x4\x3\x4\x3\x4\x3\x4\x5\x4/\n\x4\x3\x4\x3\x4\x3\x4\x3\x4");
		sb.Append("\x3\x4\x3\x4\a\x4\x37\n\x4\f\x4\xE\x4:\v\x4\x3\x5\x3\x5\x3\x5");
		sb.Append("\x5\x5?\n\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\a\x6");
		sb.Append("H\n\x6\f\x6\xE\x6K\v\x6\x5\x6M\n\x6\x3\x6\x3\x6\x3\a\x3\a\x3");
		sb.Append("\a\x2\x4\x4\x6\b\x2\x4\x6\b\n\f\x2\x5\x3\x2\x12\x13\x3\x2\x10");
		sb.Append("\x11\x5\x2\x5\x5\b\b\v\r[\x2\x10\x3\x2\x2\x2\x4\x18\x3\x2\x2");
		sb.Append("\x2\x6.\x3\x2\x2\x2\b;\x3\x2\x2\x2\n\x42\x3\x2\x2\x2\fP\x3\x2");
		sb.Append("\x2\x2\xE\x11\x5\x4\x3\x2\xF\x11\x5\x6\x4\x2\x10\xE\x3\x2\x2");
		sb.Append("\x2\x10\xF\x3\x2\x2\x2\x11\x3\x3\x2\x2\x2\x12\x13\b\x3\x1\x2");
		sb.Append("\x13\x14\t\x2\x2\x2\x14\x19\x5\x4\x3\t\x15\x19\x5\b\x5\x2\x16");
		sb.Append("\x19\x5\n\x6\x2\x17\x19\x5\f\a\x2\x18\x12\x3\x2\x2\x2\x18\x15");
		sb.Append("\x3\x2\x2\x2\x18\x16\x3\x2\x2\x2\x18\x17\x3\x2\x2\x2\x19%\x3");
		sb.Append("\x2\x2\x2\x1A\x1B\f\a\x2\x2\x1B\x1C\a\x14\x2\x2\x1C$\x5\x4\x3");
		sb.Append("\b\x1D\x1E\f\x6\x2\x2\x1E\x1F\t\x3\x2\x2\x1F$\x5\x4\x3\a !\f");
		sb.Append("\x5\x2\x2!\"\t\x2\x2\x2\"$\x5\x4\x3\x6#\x1A\x3\x2\x2\x2#\x1D");
		sb.Append("\x3\x2\x2\x2# \x3\x2\x2\x2$\'\x3\x2\x2\x2%#\x3\x2\x2\x2%&\x3");
		sb.Append("\x2\x2\x2&\x5\x3\x2\x2\x2\'%\x3\x2\x2\x2()\b\x4\x1\x2)*\a\x3");
		sb.Append("\x2\x2*/\x5\x6\x4\b+/\x5\b\x5\x2,/\x5\n\x6\x2-/\x5\f\a\x2.(");
		sb.Append("\x3\x2\x2\x2.+\x3\x2\x2\x2.,\x3\x2\x2\x2.-\x3\x2\x2\x2/\x38");
		sb.Append("\x3\x2\x2\x2\x30\x31\f\x6\x2\x2\x31\x32\a\x15\x2\x2\x32\x37");
		sb.Append("\x5\x6\x4\a\x33\x34\f\x5\x2\x2\x34\x35\a\x16\x2\x2\x35\x37\x5");
		sb.Append("\x6\x4\x6\x36\x30\x3\x2\x2\x2\x36\x33\x3\x2\x2\x2\x37:\x3\x2");
		sb.Append("\x2\x2\x38\x36\x3\x2\x2\x2\x38\x39\x3\x2\x2\x2\x39\a\x3\x2\x2");
		sb.Append("\x2:\x38\x3\x2\x2\x2;>\a\xE\x2\x2<?\x5\x4\x3\x2=?\x5\x6\x4\x2");
		sb.Append("><\x3\x2\x2\x2>=\x3\x2\x2\x2?@\x3\x2\x2\x2@\x41\a\xF\x2\x2\x41");
		sb.Append("\t\x3\x2\x2\x2\x42\x43\a\r\x2\x2\x43L\a\xE\x2\x2\x44I\x5\x2");
		sb.Append("\x2\x2\x45\x46\a\x4\x2\x2\x46H\x5\x2\x2\x2G\x45\x3\x2\x2\x2");
		sb.Append("HK\x3\x2\x2\x2IG\x3\x2\x2\x2IJ\x3\x2\x2\x2JM\x3\x2\x2\x2KI\x3");
		sb.Append("\x2\x2\x2L\x44\x3\x2\x2\x2LM\x3\x2\x2\x2MN\x3\x2\x2\x2NO\a\xF");
		sb.Append("\x2\x2O\v\x3\x2\x2\x2PQ\t\x4\x2\x2Q\r\x3\x2\x2\x2\f\x10\x18");
		sb.Append("#%.\x36\x38>IL");
	    return sb.ToString();
	}

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());


}
} // namespace GillSoft.ExpressionEvaluator