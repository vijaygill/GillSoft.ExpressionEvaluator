grammar JsonPath;

jsonpath : rootItem property+;

arrayIndex : '[' index = INT ']';

rootItem : DOLLAR (index = arrayIndex?);

property : (propertyInBrackets = PROPERTY_IN_BRACKETS | propertyWithDot = PROPERTY_WITH_DOT) (index = arrayIndex?);

PROPERTY_IN_BRACKETS : (BRACKET_SQ_L QUOTE_S) ( ~( '"' | '\'' | '[' | ']')*) (QUOTE_S BRACKET_SQ_R);
PROPERTY_WITH_DOT : DOT ( ~( '.' | '"' | '\'' | '[' | ']')*);

INDENTIFIER  : [a-zA-Z] ~( '.' | '"' | '\'' | '[' | ']')*;
INT          : [0-9]+;
BRACKET_SQ_L : '[';
BRACKET_SQ_R : ']';
QUOTE_S      : '\'';
DOT          : '.';
DOLLAR       : '$';
HYPHEN       : '-';
COLON        : ':';
WS           : [ \t\n\r]+ -> skip;
