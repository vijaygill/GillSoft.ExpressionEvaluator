grammar Expression;

expression : mathematicalExpression
			| booleanExprerssion
			;

mathematicalExpression :sign = ('-' | '+') expr = mathematicalExpression
			| subExpression
			| left = mathematicalExpression op = POW right = mathematicalExpression
			| left = mathematicalExpression op = ( MULT | DIV ) right = mathematicalExpression
			| left = mathematicalExpression op = ( ADD | SUB ) right = mathematicalExpression
			| functionValue = function
			| value = simpleValue
			;

booleanExprerssion: sign = '!' expr = booleanExprerssion
			| subExpression
			| left = booleanExprerssion op = AND right = booleanExprerssion
			| left = booleanExprerssion op = OR right = booleanExprerssion
			| functionValue = function
			| value = simpleValue
			;

subExpression : LPAREN (mathExpr = mathematicalExpression | boolExpr = booleanExprerssion) RPAREN ;

function : name = IDENT LPAREN  (paramFirst = expression ( ',' paramRest += expression )*)? RPAREN ;

simpleValue: value = ( IDENT  | CONST | TRUE | FALSE | STRING );

STRING  : SINGLE_QUOTED_STRING | DOUBLE_QUOTED_STRING;
SINGLE_QUOTED_STRING: '\'' (~('\'' | '\r' | '\n') ) * '\'';
DOUBLE_QUOTED_STRING: '"' (~('"' | '\r' | '\n') ) * '"';

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
ADD  : '+';
SUB  : '-';
POW  : '^';

AND  : '&&';
OR   : '||';

WS : [ \r\t\u000C\n]+ -> skip ;
