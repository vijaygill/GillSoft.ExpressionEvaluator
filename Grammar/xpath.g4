grammar xpath;

path:
	PATHSEP documentRoot = pathElement (
		PATHSEP documentChildren = pathElement
	)*;

pathElement: (axis? element filter?) | attribute;

filter:
	LBRAC (attr = attribute | elem = element) EQ value = STRING RBRAC;

axis: name = AXIS COLONCOLON;

namespacePrefix: name = IDENT;

attribute: AT (ns = namespacePrefix COLON)? name = IDENT;

element: (ns = namespacePrefix COLON)? name = IDENT;

AXIS:
	'ancestor'
	| 'ancestor-or-self'
	| 'attribute'
	| 'child'
	| 'descendant'
	| 'descendant-or-self'
	| 'following'
	| 'following-sibling'
	| 'namespace'
	| 'parent'
	| 'preceding'
	| 'preceding-sibling'
	| 'self';

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
STRING: '"' ~'"'* '"' | '\'' ~'\''* '\'';

Whitespace: (' ' | '\t' | '\n' | '\r')+ -> skip;
