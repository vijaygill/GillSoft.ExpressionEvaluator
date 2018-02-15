grammar xpath;

path: PATHSEP root = pathElement ( PATHSEP children = pathElement )*;

pathElement: (function ? element filter?) | attribute;

filter:
	LBRAC (attr = attribute | elem = element) EQ value = STRING RBRAC;

function: name = FUNCTION DOUBLE_COLON;

namespacePrefix: name = IDENT ;

attribute: AT (ns = namespacePrefix COLON)? name = IDENT;

element: (ns = namespacePrefix COLON)? name = IDENT;

FUNCTION:
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

DOUBLE_COLON : '::';

INTEGER: [0-9]+;

DECIMAL: [0-9]+ '.' [0-9]+;

fragment HASH : '#' ;
fragment HYPHEN : '-' ;
fragment UNDERSCORE : '_' ;
fragment DIGIT  : '0'..'9' ;
fragment LETTER : 'a'..'z' |'A'..'Z' ;
fragment NUMBER : DIGIT+ ;
fragment WORD : LETTER+  ;

IDENT : HASH | LETTER (LETTER | DIGIT | HYPHEN | UNDERSCORE)*;

PATHSEP: '/';
ABRPATH: '//';
LPAR: '(';
RPAR: ')';
LBRAC: '[';
RBRAC: ']';
MINUS: '-';
PLUS: '+';
DOT: '.';
MUL: '*';
DOTDOT: '..';
AT: '@';
COMMA: ',';
PIPE: '|';
LESS: '<';
MORE_: '>';
EQ: '==' | '=';
LE: '<=';
GE: '>=';
COLON: ':';
CC: '::';
APOS: '\'';
QUOT: '"';
STRING: '"' ~'"'* '"' | '\'' ~'\''* '\'';

Whitespace: (' ' | '\t' | '\n' | '\r')+ -> skip;
