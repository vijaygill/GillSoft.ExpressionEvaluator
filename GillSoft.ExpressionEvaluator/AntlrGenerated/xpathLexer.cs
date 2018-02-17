//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ..\Grammar\xpath.g4 by ANTLR 4.7.1

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
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class xpathLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, AXIS=3, AXIS_ANCESTOR=4, AXIS_ANCESTOR_OR_SELF=5, AXIS_ATTRIBUTE=6, 
		AXIS_CHILD=7, AXIS_DESCENDANT=8, AXIS_DESCENDANT_OR_SELF=9, AXIS_FOLLOWING=10, 
		AXIS_NAMESPACE=11, AXIS_PARENT=12, AXIS_PRECEDING=13, AXIS_PRECEDING_SIBLING=14, 
		AXIS_FOLLOWNG_SUBLING=15, AXIS_SELF=16, IDENT=17, PATHSEP=18, LBRAC=19, 
		RBRAC=20, AT=21, EQ=22, COLON=23, COLONCOLON=24, Whitespace=25;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "AXIS", "AXIS_ANCESTOR", "AXIS_ANCESTOR_OR_SELF", "AXIS_ATTRIBUTE", 
		"AXIS_CHILD", "AXIS_DESCENDANT", "AXIS_DESCENDANT_OR_SELF", "AXIS_FOLLOWING", 
		"AXIS_NAMESPACE", "AXIS_PARENT", "AXIS_PRECEDING", "AXIS_PRECEDING_SIBLING", 
		"AXIS_FOLLOWNG_SUBLING", "AXIS_SELF", "HASH", "HYPHEN", "UNDERSCORE", 
		"DIGIT", "LETTER", "NUMBER", "WORD", "DOT", "IDENT", "PATHSEP", "LBRAC", 
		"RBRAC", "AT", "EQ", "COLON", "COLONCOLON", "Whitespace"
	};


	public xpathLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public xpathLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'''", "'\"'", null, "'ancestor'", "'ancestor-or-self'", "'attribute'", 
		"'child'", "'descendant'", "'descendant-or-self'", "'following'", "'namespace'", 
		"'parent'", "'preceding'", "'preceding-sibling'", "'following-sibling'", 
		"'self'", null, "'/'", "'['", "']'", "'@'", null, "':'", "'::'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, "AXIS", "AXIS_ANCESTOR", "AXIS_ANCESTOR_OR_SELF", "AXIS_ATTRIBUTE", 
		"AXIS_CHILD", "AXIS_DESCENDANT", "AXIS_DESCENDANT_OR_SELF", "AXIS_FOLLOWING", 
		"AXIS_NAMESPACE", "AXIS_PARENT", "AXIS_PRECEDING", "AXIS_PRECEDING_SIBLING", 
		"AXIS_FOLLOWNG_SUBLING", "AXIS_SELF", "IDENT", "PATHSEP", "LBRAC", "RBRAC", 
		"AT", "EQ", "COLON", "COLONCOLON", "Whitespace"
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

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static xpathLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\x1B', '\x12B', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x5', '\x4', 'W', '\n', '\x4', '\x3', '\x5', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\b', '\x3', 
		'\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\t', 
		'\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', 
		'\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\n', 
		'\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', 
		'\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', 
		'\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', 
		'\n', '\x3', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', '\v', 
		'\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', 
		'\v', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', 
		'\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', 
		'\r', '\x3', '\r', '\x3', '\r', '\x3', '\r', '\x3', '\r', '\x3', '\r', 
		'\x3', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', 
		'\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', 
		'\xE', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', 
		'\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', 
		'\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', 
		'\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\x10', '\x3', 
		'\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', 
		'\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', 
		'\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', 
		'\x10', '\x3', '\x10', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', 
		'\x11', '\x3', '\x11', '\x3', '\x12', '\x3', '\x12', '\x3', '\x13', '\x3', 
		'\x13', '\x3', '\x14', '\x3', '\x14', '\x3', '\x15', '\x3', '\x15', '\x3', 
		'\x16', '\x3', '\x16', '\x3', '\x17', '\x6', '\x17', '\xFA', '\n', '\x17', 
		'\r', '\x17', '\xE', '\x17', '\xFB', '\x3', '\x18', '\x6', '\x18', '\xFF', 
		'\n', '\x18', '\r', '\x18', '\xE', '\x18', '\x100', '\x3', '\x19', '\x3', 
		'\x19', '\x3', '\x1A', '\x3', '\x1A', '\x3', '\x1A', '\x3', '\x1A', '\x3', 
		'\x1A', '\x3', '\x1A', '\x3', '\x1A', '\a', '\x1A', '\x10C', '\n', '\x1A', 
		'\f', '\x1A', '\xE', '\x1A', '\x10F', '\v', '\x1A', '\x5', '\x1A', '\x111', 
		'\n', '\x1A', '\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1C', '\x3', '\x1C', 
		'\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1F', 
		'\x3', '\x1F', '\x3', '\x1F', '\x5', '\x1F', '\x11E', '\n', '\x1F', '\x3', 
		' ', '\x3', ' ', '\x3', '!', '\x3', '!', '\x3', '!', '\x3', '\"', '\x6', 
		'\"', '\x126', '\n', '\"', '\r', '\"', '\xE', '\"', '\x127', '\x3', '\"', 
		'\x3', '\"', '\x2', '\x2', '#', '\x3', '\x3', '\x5', '\x4', '\a', '\x5', 
		'\t', '\x6', '\v', '\a', '\r', '\b', '\xF', '\t', '\x11', '\n', '\x13', 
		'\v', '\x15', '\f', '\x17', '\r', '\x19', '\xE', '\x1B', '\xF', '\x1D', 
		'\x10', '\x1F', '\x11', '!', '\x12', '#', '\x2', '%', '\x2', '\'', '\x2', 
		')', '\x2', '+', '\x2', '-', '\x2', '/', '\x2', '\x31', '\x2', '\x33', 
		'\x13', '\x35', '\x14', '\x37', '\x15', '\x39', '\x16', ';', '\x17', '=', 
		'\x18', '?', '\x19', '\x41', '\x1A', '\x43', '\x1B', '\x3', '\x2', '\x4', 
		'\x4', '\x2', '\x43', '\\', '\x63', '|', '\x5', '\x2', '\v', '\f', '\xF', 
		'\xF', '\"', '\"', '\x2', '\x138', '\x2', '\x3', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x5', '\x3', '\x2', '\x2', '\x2', '\x2', '\a', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\t', '\x3', '\x2', '\x2', '\x2', '\x2', '\v', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\r', '\x3', '\x2', '\x2', '\x2', '\x2', '\xF', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x11', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x13', '\x3', '\x2', '\x2', '\x2', '\x2', '\x15', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x17', '\x3', '\x2', '\x2', '\x2', '\x2', '\x19', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1D', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '!', '\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x37', '\x3', '\x2', '\x2', '\x2', '\x2', '\x39', '\x3', '\x2', '\x2', 
		'\x2', '\x2', ';', '\x3', '\x2', '\x2', '\x2', '\x2', '=', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '?', '\x3', '\x2', '\x2', '\x2', '\x2', '\x41', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x43', '\x3', '\x2', '\x2', '\x2', '\x3', 
		'\x45', '\x3', '\x2', '\x2', '\x2', '\x5', 'G', '\x3', '\x2', '\x2', '\x2', 
		'\a', 'V', '\x3', '\x2', '\x2', '\x2', '\t', 'X', '\x3', '\x2', '\x2', 
		'\x2', '\v', '\x61', '\x3', '\x2', '\x2', '\x2', '\r', 'r', '\x3', '\x2', 
		'\x2', '\x2', '\xF', '|', '\x3', '\x2', '\x2', '\x2', '\x11', '\x82', 
		'\x3', '\x2', '\x2', '\x2', '\x13', '\x8D', '\x3', '\x2', '\x2', '\x2', 
		'\x15', '\xA0', '\x3', '\x2', '\x2', '\x2', '\x17', '\xAA', '\x3', '\x2', 
		'\x2', '\x2', '\x19', '\xB4', '\x3', '\x2', '\x2', '\x2', '\x1B', '\xBB', 
		'\x3', '\x2', '\x2', '\x2', '\x1D', '\xC5', '\x3', '\x2', '\x2', '\x2', 
		'\x1F', '\xD7', '\x3', '\x2', '\x2', '\x2', '!', '\xE9', '\x3', '\x2', 
		'\x2', '\x2', '#', '\xEE', '\x3', '\x2', '\x2', '\x2', '%', '\xF0', '\x3', 
		'\x2', '\x2', '\x2', '\'', '\xF2', '\x3', '\x2', '\x2', '\x2', ')', '\xF4', 
		'\x3', '\x2', '\x2', '\x2', '+', '\xF6', '\x3', '\x2', '\x2', '\x2', '-', 
		'\xF9', '\x3', '\x2', '\x2', '\x2', '/', '\xFE', '\x3', '\x2', '\x2', 
		'\x2', '\x31', '\x102', '\x3', '\x2', '\x2', '\x2', '\x33', '\x110', '\x3', 
		'\x2', '\x2', '\x2', '\x35', '\x112', '\x3', '\x2', '\x2', '\x2', '\x37', 
		'\x114', '\x3', '\x2', '\x2', '\x2', '\x39', '\x116', '\x3', '\x2', '\x2', 
		'\x2', ';', '\x118', '\x3', '\x2', '\x2', '\x2', '=', '\x11D', '\x3', 
		'\x2', '\x2', '\x2', '?', '\x11F', '\x3', '\x2', '\x2', '\x2', '\x41', 
		'\x121', '\x3', '\x2', '\x2', '\x2', '\x43', '\x125', '\x3', '\x2', '\x2', 
		'\x2', '\x45', '\x46', '\a', ')', '\x2', '\x2', '\x46', '\x4', '\x3', 
		'\x2', '\x2', '\x2', 'G', 'H', '\a', '$', '\x2', '\x2', 'H', '\x6', '\x3', 
		'\x2', '\x2', '\x2', 'I', 'W', '\x5', '\t', '\x5', '\x2', 'J', 'W', '\x5', 
		'\v', '\x6', '\x2', 'K', 'W', '\x5', '\r', '\a', '\x2', 'L', 'W', '\x5', 
		'\xF', '\b', '\x2', 'M', 'W', '\x5', '\x11', '\t', '\x2', 'N', 'W', '\x5', 
		'\x13', '\n', '\x2', 'O', 'W', '\x5', '\x15', '\v', '\x2', 'P', 'W', '\x5', 
		'\x1F', '\x10', '\x2', 'Q', 'W', '\x5', '\x17', '\f', '\x2', 'R', 'W', 
		'\x5', '\x19', '\r', '\x2', 'S', 'W', '\x5', '\x1B', '\xE', '\x2', 'T', 
		'W', '\x5', '\x1D', '\xF', '\x2', 'U', 'W', '\x5', '!', '\x11', '\x2', 
		'V', 'I', '\x3', '\x2', '\x2', '\x2', 'V', 'J', '\x3', '\x2', '\x2', '\x2', 
		'V', 'K', '\x3', '\x2', '\x2', '\x2', 'V', 'L', '\x3', '\x2', '\x2', '\x2', 
		'V', 'M', '\x3', '\x2', '\x2', '\x2', 'V', 'N', '\x3', '\x2', '\x2', '\x2', 
		'V', 'O', '\x3', '\x2', '\x2', '\x2', 'V', 'P', '\x3', '\x2', '\x2', '\x2', 
		'V', 'Q', '\x3', '\x2', '\x2', '\x2', 'V', 'R', '\x3', '\x2', '\x2', '\x2', 
		'V', 'S', '\x3', '\x2', '\x2', '\x2', 'V', 'T', '\x3', '\x2', '\x2', '\x2', 
		'V', 'U', '\x3', '\x2', '\x2', '\x2', 'W', '\b', '\x3', '\x2', '\x2', 
		'\x2', 'X', 'Y', '\a', '\x63', '\x2', '\x2', 'Y', 'Z', '\a', 'p', '\x2', 
		'\x2', 'Z', '[', '\a', '\x65', '\x2', '\x2', '[', '\\', '\a', 'g', '\x2', 
		'\x2', '\\', ']', '\a', 'u', '\x2', '\x2', ']', '^', '\a', 'v', '\x2', 
		'\x2', '^', '_', '\a', 'q', '\x2', '\x2', '_', '`', '\a', 't', '\x2', 
		'\x2', '`', '\n', '\x3', '\x2', '\x2', '\x2', '\x61', '\x62', '\a', '\x63', 
		'\x2', '\x2', '\x62', '\x63', '\a', 'p', '\x2', '\x2', '\x63', '\x64', 
		'\a', '\x65', '\x2', '\x2', '\x64', '\x65', '\a', 'g', '\x2', '\x2', '\x65', 
		'\x66', '\a', 'u', '\x2', '\x2', '\x66', 'g', '\a', 'v', '\x2', '\x2', 
		'g', 'h', '\a', 'q', '\x2', '\x2', 'h', 'i', '\a', 't', '\x2', '\x2', 
		'i', 'j', '\a', '/', '\x2', '\x2', 'j', 'k', '\a', 'q', '\x2', '\x2', 
		'k', 'l', '\a', 't', '\x2', '\x2', 'l', 'm', '\a', '/', '\x2', '\x2', 
		'm', 'n', '\a', 'u', '\x2', '\x2', 'n', 'o', '\a', 'g', '\x2', '\x2', 
		'o', 'p', '\a', 'n', '\x2', '\x2', 'p', 'q', '\a', 'h', '\x2', '\x2', 
		'q', '\f', '\x3', '\x2', '\x2', '\x2', 'r', 's', '\a', '\x63', '\x2', 
		'\x2', 's', 't', '\a', 'v', '\x2', '\x2', 't', 'u', '\a', 'v', '\x2', 
		'\x2', 'u', 'v', '\a', 't', '\x2', '\x2', 'v', 'w', '\a', 'k', '\x2', 
		'\x2', 'w', 'x', '\a', '\x64', '\x2', '\x2', 'x', 'y', '\a', 'w', '\x2', 
		'\x2', 'y', 'z', '\a', 'v', '\x2', '\x2', 'z', '{', '\a', 'g', '\x2', 
		'\x2', '{', '\xE', '\x3', '\x2', '\x2', '\x2', '|', '}', '\a', '\x65', 
		'\x2', '\x2', '}', '~', '\a', 'j', '\x2', '\x2', '~', '\x7F', '\a', 'k', 
		'\x2', '\x2', '\x7F', '\x80', '\a', 'n', '\x2', '\x2', '\x80', '\x81', 
		'\a', '\x66', '\x2', '\x2', '\x81', '\x10', '\x3', '\x2', '\x2', '\x2', 
		'\x82', '\x83', '\a', '\x66', '\x2', '\x2', '\x83', '\x84', '\a', 'g', 
		'\x2', '\x2', '\x84', '\x85', '\a', 'u', '\x2', '\x2', '\x85', '\x86', 
		'\a', '\x65', '\x2', '\x2', '\x86', '\x87', '\a', 'g', '\x2', '\x2', '\x87', 
		'\x88', '\a', 'p', '\x2', '\x2', '\x88', '\x89', '\a', '\x66', '\x2', 
		'\x2', '\x89', '\x8A', '\a', '\x63', '\x2', '\x2', '\x8A', '\x8B', '\a', 
		'p', '\x2', '\x2', '\x8B', '\x8C', '\a', 'v', '\x2', '\x2', '\x8C', '\x12', 
		'\x3', '\x2', '\x2', '\x2', '\x8D', '\x8E', '\a', '\x66', '\x2', '\x2', 
		'\x8E', '\x8F', '\a', 'g', '\x2', '\x2', '\x8F', '\x90', '\a', 'u', '\x2', 
		'\x2', '\x90', '\x91', '\a', '\x65', '\x2', '\x2', '\x91', '\x92', '\a', 
		'g', '\x2', '\x2', '\x92', '\x93', '\a', 'p', '\x2', '\x2', '\x93', '\x94', 
		'\a', '\x66', '\x2', '\x2', '\x94', '\x95', '\a', '\x63', '\x2', '\x2', 
		'\x95', '\x96', '\a', 'p', '\x2', '\x2', '\x96', '\x97', '\a', 'v', '\x2', 
		'\x2', '\x97', '\x98', '\a', '/', '\x2', '\x2', '\x98', '\x99', '\a', 
		'q', '\x2', '\x2', '\x99', '\x9A', '\a', 't', '\x2', '\x2', '\x9A', '\x9B', 
		'\a', '/', '\x2', '\x2', '\x9B', '\x9C', '\a', 'u', '\x2', '\x2', '\x9C', 
		'\x9D', '\a', 'g', '\x2', '\x2', '\x9D', '\x9E', '\a', 'n', '\x2', '\x2', 
		'\x9E', '\x9F', '\a', 'h', '\x2', '\x2', '\x9F', '\x14', '\x3', '\x2', 
		'\x2', '\x2', '\xA0', '\xA1', '\a', 'h', '\x2', '\x2', '\xA1', '\xA2', 
		'\a', 'q', '\x2', '\x2', '\xA2', '\xA3', '\a', 'n', '\x2', '\x2', '\xA3', 
		'\xA4', '\a', 'n', '\x2', '\x2', '\xA4', '\xA5', '\a', 'q', '\x2', '\x2', 
		'\xA5', '\xA6', '\a', 'y', '\x2', '\x2', '\xA6', '\xA7', '\a', 'k', '\x2', 
		'\x2', '\xA7', '\xA8', '\a', 'p', '\x2', '\x2', '\xA8', '\xA9', '\a', 
		'i', '\x2', '\x2', '\xA9', '\x16', '\x3', '\x2', '\x2', '\x2', '\xAA', 
		'\xAB', '\a', 'p', '\x2', '\x2', '\xAB', '\xAC', '\a', '\x63', '\x2', 
		'\x2', '\xAC', '\xAD', '\a', 'o', '\x2', '\x2', '\xAD', '\xAE', '\a', 
		'g', '\x2', '\x2', '\xAE', '\xAF', '\a', 'u', '\x2', '\x2', '\xAF', '\xB0', 
		'\a', 'r', '\x2', '\x2', '\xB0', '\xB1', '\a', '\x63', '\x2', '\x2', '\xB1', 
		'\xB2', '\a', '\x65', '\x2', '\x2', '\xB2', '\xB3', '\a', 'g', '\x2', 
		'\x2', '\xB3', '\x18', '\x3', '\x2', '\x2', '\x2', '\xB4', '\xB5', '\a', 
		'r', '\x2', '\x2', '\xB5', '\xB6', '\a', '\x63', '\x2', '\x2', '\xB6', 
		'\xB7', '\a', 't', '\x2', '\x2', '\xB7', '\xB8', '\a', 'g', '\x2', '\x2', 
		'\xB8', '\xB9', '\a', 'p', '\x2', '\x2', '\xB9', '\xBA', '\a', 'v', '\x2', 
		'\x2', '\xBA', '\x1A', '\x3', '\x2', '\x2', '\x2', '\xBB', '\xBC', '\a', 
		'r', '\x2', '\x2', '\xBC', '\xBD', '\a', 't', '\x2', '\x2', '\xBD', '\xBE', 
		'\a', 'g', '\x2', '\x2', '\xBE', '\xBF', '\a', '\x65', '\x2', '\x2', '\xBF', 
		'\xC0', '\a', 'g', '\x2', '\x2', '\xC0', '\xC1', '\a', '\x66', '\x2', 
		'\x2', '\xC1', '\xC2', '\a', 'k', '\x2', '\x2', '\xC2', '\xC3', '\a', 
		'p', '\x2', '\x2', '\xC3', '\xC4', '\a', 'i', '\x2', '\x2', '\xC4', '\x1C', 
		'\x3', '\x2', '\x2', '\x2', '\xC5', '\xC6', '\a', 'r', '\x2', '\x2', '\xC6', 
		'\xC7', '\a', 't', '\x2', '\x2', '\xC7', '\xC8', '\a', 'g', '\x2', '\x2', 
		'\xC8', '\xC9', '\a', '\x65', '\x2', '\x2', '\xC9', '\xCA', '\a', 'g', 
		'\x2', '\x2', '\xCA', '\xCB', '\a', '\x66', '\x2', '\x2', '\xCB', '\xCC', 
		'\a', 'k', '\x2', '\x2', '\xCC', '\xCD', '\a', 'p', '\x2', '\x2', '\xCD', 
		'\xCE', '\a', 'i', '\x2', '\x2', '\xCE', '\xCF', '\a', '/', '\x2', '\x2', 
		'\xCF', '\xD0', '\a', 'u', '\x2', '\x2', '\xD0', '\xD1', '\a', 'k', '\x2', 
		'\x2', '\xD1', '\xD2', '\a', '\x64', '\x2', '\x2', '\xD2', '\xD3', '\a', 
		'n', '\x2', '\x2', '\xD3', '\xD4', '\a', 'k', '\x2', '\x2', '\xD4', '\xD5', 
		'\a', 'p', '\x2', '\x2', '\xD5', '\xD6', '\a', 'i', '\x2', '\x2', '\xD6', 
		'\x1E', '\x3', '\x2', '\x2', '\x2', '\xD7', '\xD8', '\a', 'h', '\x2', 
		'\x2', '\xD8', '\xD9', '\a', 'q', '\x2', '\x2', '\xD9', '\xDA', '\a', 
		'n', '\x2', '\x2', '\xDA', '\xDB', '\a', 'n', '\x2', '\x2', '\xDB', '\xDC', 
		'\a', 'q', '\x2', '\x2', '\xDC', '\xDD', '\a', 'y', '\x2', '\x2', '\xDD', 
		'\xDE', '\a', 'k', '\x2', '\x2', '\xDE', '\xDF', '\a', 'p', '\x2', '\x2', 
		'\xDF', '\xE0', '\a', 'i', '\x2', '\x2', '\xE0', '\xE1', '\a', '/', '\x2', 
		'\x2', '\xE1', '\xE2', '\a', 'u', '\x2', '\x2', '\xE2', '\xE3', '\a', 
		'k', '\x2', '\x2', '\xE3', '\xE4', '\a', '\x64', '\x2', '\x2', '\xE4', 
		'\xE5', '\a', 'n', '\x2', '\x2', '\xE5', '\xE6', '\a', 'k', '\x2', '\x2', 
		'\xE6', '\xE7', '\a', 'p', '\x2', '\x2', '\xE7', '\xE8', '\a', 'i', '\x2', 
		'\x2', '\xE8', ' ', '\x3', '\x2', '\x2', '\x2', '\xE9', '\xEA', '\a', 
		'u', '\x2', '\x2', '\xEA', '\xEB', '\a', 'g', '\x2', '\x2', '\xEB', '\xEC', 
		'\a', 'n', '\x2', '\x2', '\xEC', '\xED', '\a', 'h', '\x2', '\x2', '\xED', 
		'\"', '\x3', '\x2', '\x2', '\x2', '\xEE', '\xEF', '\a', '%', '\x2', '\x2', 
		'\xEF', '$', '\x3', '\x2', '\x2', '\x2', '\xF0', '\xF1', '\a', '/', '\x2', 
		'\x2', '\xF1', '&', '\x3', '\x2', '\x2', '\x2', '\xF2', '\xF3', '\a', 
		'\x61', '\x2', '\x2', '\xF3', '(', '\x3', '\x2', '\x2', '\x2', '\xF4', 
		'\xF5', '\x4', '\x32', ';', '\x2', '\xF5', '*', '\x3', '\x2', '\x2', '\x2', 
		'\xF6', '\xF7', '\t', '\x2', '\x2', '\x2', '\xF7', ',', '\x3', '\x2', 
		'\x2', '\x2', '\xF8', '\xFA', '\x5', ')', '\x15', '\x2', '\xF9', '\xF8', 
		'\x3', '\x2', '\x2', '\x2', '\xFA', '\xFB', '\x3', '\x2', '\x2', '\x2', 
		'\xFB', '\xF9', '\x3', '\x2', '\x2', '\x2', '\xFB', '\xFC', '\x3', '\x2', 
		'\x2', '\x2', '\xFC', '.', '\x3', '\x2', '\x2', '\x2', '\xFD', '\xFF', 
		'\x5', '+', '\x16', '\x2', '\xFE', '\xFD', '\x3', '\x2', '\x2', '\x2', 
		'\xFF', '\x100', '\x3', '\x2', '\x2', '\x2', '\x100', '\xFE', '\x3', '\x2', 
		'\x2', '\x2', '\x100', '\x101', '\x3', '\x2', '\x2', '\x2', '\x101', '\x30', 
		'\x3', '\x2', '\x2', '\x2', '\x102', '\x103', '\a', '\x30', '\x2', '\x2', 
		'\x103', '\x32', '\x3', '\x2', '\x2', '\x2', '\x104', '\x111', '\x5', 
		'#', '\x12', '\x2', '\x105', '\x10D', '\x5', '+', '\x16', '\x2', '\x106', 
		'\x10C', '\x5', '+', '\x16', '\x2', '\x107', '\x10C', '\x5', ')', '\x15', 
		'\x2', '\x108', '\x10C', '\x5', '%', '\x13', '\x2', '\x109', '\x10C', 
		'\x5', '\'', '\x14', '\x2', '\x10A', '\x10C', '\x5', '\x31', '\x19', '\x2', 
		'\x10B', '\x106', '\x3', '\x2', '\x2', '\x2', '\x10B', '\x107', '\x3', 
		'\x2', '\x2', '\x2', '\x10B', '\x108', '\x3', '\x2', '\x2', '\x2', '\x10B', 
		'\x109', '\x3', '\x2', '\x2', '\x2', '\x10B', '\x10A', '\x3', '\x2', '\x2', 
		'\x2', '\x10C', '\x10F', '\x3', '\x2', '\x2', '\x2', '\x10D', '\x10B', 
		'\x3', '\x2', '\x2', '\x2', '\x10D', '\x10E', '\x3', '\x2', '\x2', '\x2', 
		'\x10E', '\x111', '\x3', '\x2', '\x2', '\x2', '\x10F', '\x10D', '\x3', 
		'\x2', '\x2', '\x2', '\x110', '\x104', '\x3', '\x2', '\x2', '\x2', '\x110', 
		'\x105', '\x3', '\x2', '\x2', '\x2', '\x111', '\x34', '\x3', '\x2', '\x2', 
		'\x2', '\x112', '\x113', '\a', '\x31', '\x2', '\x2', '\x113', '\x36', 
		'\x3', '\x2', '\x2', '\x2', '\x114', '\x115', '\a', ']', '\x2', '\x2', 
		'\x115', '\x38', '\x3', '\x2', '\x2', '\x2', '\x116', '\x117', '\a', '_', 
		'\x2', '\x2', '\x117', ':', '\x3', '\x2', '\x2', '\x2', '\x118', '\x119', 
		'\a', '\x42', '\x2', '\x2', '\x119', '<', '\x3', '\x2', '\x2', '\x2', 
		'\x11A', '\x11B', '\a', '?', '\x2', '\x2', '\x11B', '\x11E', '\a', '?', 
		'\x2', '\x2', '\x11C', '\x11E', '\a', '?', '\x2', '\x2', '\x11D', '\x11A', 
		'\x3', '\x2', '\x2', '\x2', '\x11D', '\x11C', '\x3', '\x2', '\x2', '\x2', 
		'\x11E', '>', '\x3', '\x2', '\x2', '\x2', '\x11F', '\x120', '\a', '<', 
		'\x2', '\x2', '\x120', '@', '\x3', '\x2', '\x2', '\x2', '\x121', '\x122', 
		'\a', '<', '\x2', '\x2', '\x122', '\x123', '\a', '<', '\x2', '\x2', '\x123', 
		'\x42', '\x3', '\x2', '\x2', '\x2', '\x124', '\x126', '\t', '\x3', '\x2', 
		'\x2', '\x125', '\x124', '\x3', '\x2', '\x2', '\x2', '\x126', '\x127', 
		'\x3', '\x2', '\x2', '\x2', '\x127', '\x125', '\x3', '\x2', '\x2', '\x2', 
		'\x127', '\x128', '\x3', '\x2', '\x2', '\x2', '\x128', '\x129', '\x3', 
		'\x2', '\x2', '\x2', '\x129', '\x12A', '\b', '\"', '\x2', '\x2', '\x12A', 
		'\x44', '\x3', '\x2', '\x2', '\x2', '\v', '\x2', 'V', '\xFB', '\x100', 
		'\x10B', '\x10D', '\x110', '\x11D', '\x127', '\x3', '\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace GillSoft.ExpressionEvaluator
