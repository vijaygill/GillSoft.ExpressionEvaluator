grammar xpath;

path: (PATHSEP pathElement)+;

pathElement: ax = axis ? elem = element (LBRAC filt = expression RBRAC)?
				| attr = attribute;

expression: LPAREN subExpr = expression RPAREN
		| left = expression (AND | OR) right = expression
		| attr = attribute (NOT_EQ | EQ) value = string
		| constVal = (TRUE | FALSE);

axis: name = AXIS COLONCOLON;

namespacePrefix: name = IDENT;

attribute: AT (ns = namespacePrefix COLON)? name = IDENT;

element: (ns = namespacePrefix COLON)? name = IDENT;

string: stringSingleQuote | stringDoubleQuote;

stringSingleQuote: '\'' (~'\'')* '\'';
stringDoubleQuote: '"' (~'"')* '"';

unknowns: Unknown*;

AND: 'and';
OR: 'or';
NOT_EQ: '!=';
TRUE: 'true';
FALSE: 'false';

AXIS:
	AXIS_ANCESTOR
	| AXIS_ANCESTOR_OR_SELF
	| AXIS_ATTRIBUTE
	| AXIS_CHILD
	| AXIS_DESCENDANT
	| AXIS_DESCENDANT_OR_SELF
	| AXIS_FOLLOWING
	| AXIS_FOLLOWNG_SUBLING
	| AXIS_NAMESPACE
	| AXIS_PARENT
	| AXIS_PRECEDING
	| AXIS_PRECEDING_SIBLING
	| AXIS_SELF;

AXIS_ANCESTOR: 'ancestor';
AXIS_ANCESTOR_OR_SELF: 'ancestor-or-self';
AXIS_ATTRIBUTE: 'attribute';
AXIS_CHILD: 'child';
AXIS_DESCENDANT: 'descendant';
AXIS_DESCENDANT_OR_SELF: 'descendant-or-self';
AXIS_FOLLOWING: 'following';
AXIS_NAMESPACE: 'namespace';
AXIS_PARENT: 'parent';
AXIS_PRECEDING: 'preceding';
AXIS_PRECEDING_SIBLING: 'preceding-sibling';
AXIS_FOLLOWNG_SUBLING: 'following-sibling';
AXIS_SELF: 'self';

fragment HASH: '#';
fragment HYPHEN: '-';
fragment UNDERSCORE: '_';
fragment DIGIT: '0' ..'9';
fragment LETTER: 'a' ..'z' | 'A' ..'Z';
fragment NUMBER: DIGIT+;
fragment WORD: LETTER+;
fragment DOT: '.';

IDENT:
	HASH
	| LETTER (LETTER | DIGIT | HYPHEN | UNDERSCORE | DOT)*;

PATHSEP: '/';
LBRAC: '[';
RBRAC: ']';
AT: '@';
EQ: '==' | '=';
COLON: ':';
COLONCOLON: '::';
LPAREN : '(';
RPAREN : ')';

WS: (' ' | '\t' ) -> skip;
Whitespace: ('\n' | '\r') -> skip;

Unknown: .;