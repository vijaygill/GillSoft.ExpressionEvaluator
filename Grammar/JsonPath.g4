grammar JsonPath;

jsonpath: rootItem ref+;

ref : (arrayItem | property);

rootItem : (rootItemArrayItem | rootItemSimple);

rootItemArrayItem: DOLLAR arrayIndex;

rootItemSimple: DOLLAR;

arrayItem: prop = property arrayIndex ;

property : (propertyQuoted) | (propertySimple);

propertyQuoted: ( DOT? '[\'' name = propertyNameQuoted '\']' );

propertySimple: DOT name = propertyNameSimple;

arrayIndex : '[' index = INT ']';

propertyNameQuoted: INDENTIFIER ( (DOT | HYPHEN | COLON) INDENTIFIER )*;

propertyNameSimple : INDENTIFIER;

INDENTIFIER : [a-zA-Z][a-zA-Z0-9]* ;
INT         : [0-9]+ ;
DOT			: '.';
DOLLAR		: '$';
HYPHEN		: '-';
COLON		: ':';
WS  :   [ \t\n\r]+ -> skip ;
