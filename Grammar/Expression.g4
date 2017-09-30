grammar Expression;

expression : mathematicalExpression
			| booleanExprerssion
			;

mathematicalExpression :sign = ( MINUS | PLUS) expr = mathematicalExpression
			| subExpression
			| left = mathematicalExpression op = POW right = mathematicalExpression
			| left = mathematicalExpression op = ( MULT | DIV ) right = mathematicalExpression
			| left = mathematicalExpression op = ( PLUS | MINUS ) right = mathematicalExpression
			| functionValue = function
			| str = stringValue
			| value = simpleValue
			;

booleanExprerssion: sign = NOT expr = booleanExprerssion
			| subExpression
			| left = booleanExprerssion op = AND right = booleanExprerssion
			| left = booleanExprerssion op = OR right = booleanExprerssion
			| functionValue = function
			| str = stringValue
			| value = simpleValue
			;

subExpression : LPAREN (mathExpr = mathematicalExpression | boolExpr = booleanExprerssion) RPAREN ;

function : name = IDENT LPAREN  (paramFirst = expression ( ',' paramRest += expression )*)? RPAREN ;

stringValue : SINGLE_QUOTED_STRING | DOUBLE_QUOTED_STRING;

SINGLE_QUOTED_STRING: '\'' (~('\'' | '\r' | '\n') ) * '\'';
DOUBLE_QUOTED_STRING: '"' (~('"' | '\r' | '\n') ) * '"';

simpleValue: value = ( IDENT  | CONST | TRUE | FALSE );

CONST	: INTEGER | DECIMAL;

INTEGER : [0-9]+;

DECIMAL : [0-9]+'.'[0-9]+;

TRUE    : [Tt][Rr][Uu][Ee];
FALSE   : [Ff][Aa][Ll][Ss][Ee];


IDENT : [_#A-Za-z][_#.A-Za-z0-9]*;

LPAREN : '(';
RPAREN : ')';

MULT : '*';
DIV  : '/';
PLUS  : '+';
MINUS  : '-';
POW  : '^';

AND  : '&&';
OR   : '||';
NOT  : '!';

WS : [ \r\t\u000C\n]+ -> skip ;
