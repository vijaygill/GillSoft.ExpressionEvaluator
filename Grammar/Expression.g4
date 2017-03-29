grammar Expression;

expression : sign = ('-' | '+') expression
			| subExpresion
			| left = expression op = POW right = expression
			| left = expression op = ( MULT | DIV ) right = expression
			| left = expression op = ( ADD | SUB ) right = expression
			| function
			| value = ( IDENT  | CONST )
			;

subExpresion : LPAREN expression RPAREN ;

function : name = IDENT LPAREN  paramFirst = expression ( ',' paramRest += expression )* RPAREN ;

CONST	: INTEGER | DECIMAL;

INTEGER : [0-9]+;

DECIMAL : [0-9]+'.'[0-9]+;

IDENT : [_#A-Za-z][_#.A-Za-z0-9]*;

LPAREN : '(';
RPAREN : ')';

MULT : '*';
DIV  : '/';
ADD  : '+';
SUB  : '-';
POW  : '^';

WS : [ \r\t\u000C\n]+ -> skip ;
