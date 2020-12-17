grammar JsonPath;

jsonpath : rootItem property*;

rootItem : DOLLAR (index = arrayIndex?);

property : (prop = propertyType) (index = arrayIndex?);

propertyType: (propertyWithBrackets = propertyWithBracketsRule)
    | (propertyWithDot = propertyWithDotRule)
    | (propertyWithDotAndBracket = propertyWithDotAndBracketRule)
    ;

propertyWithDotAndBracketRule : ID_WITH_DOT_BRACKET;
propertyWithBracketsRule      : ID_WITH_BRACKET;
propertyWithDotRule           : ID_WITHOUT_DOT;

arrayIndex : BRACKET_SQ_L index = INT BRACKET_SQ_R;

fragment DOT_BRACKET_START : '.[\'';
fragment BRACKET_START     : '[\'';
fragment BRACKET_END       : '\']';
fragment DOT               : '.';
fragment ID                : ~('$' | '[' | ']' | '.' | '\'')+;

INT                 : [0-9]+;
ID_WITH_DOT_BRACKET : DOT_BRACKET_START ID (DOT ID)* BRACKET_END;
ID_WITH_BRACKET     : BRACKET_START ID (DOT ID)* BRACKET_END;
ID_WITHOUT_DOT      : DOT ID;
BRACKET_SQ_L        : '[';
BRACKET_SQ_R        : ']';
DOLLAR              : '$';
WS                  : [ \t\n\r]+ -> skip;
