grammar xpath;

path : pathElement+;

pathElement : (PATHSEP | PATHSEP PATHSEP ) element;

filter : LBRAC attr = attribute EQ value = STRING RBRAC;

attribute : AT ((ns = IDENT COLON name = IDENT) | (name = IDENT));

element : ((ns = IDENT COLON name = IDENT) | (name = IDENT)) (filter)?;
            
INTEGER : [0-9]+;

DECIMAL : [0-9]+'.'[0-9]+;

IDENT : [_#A-Za-z][_#.A-Za-z0-9]*;

  PATHSEP 
       :'/';
  ABRPATH   
       : '//';
  LPAR   
       : '(';
  RPAR   
       : ')';
  LBRAC   
       :  '[';
  RBRAC   
       :  ']';
  MINUS   
       :  '-';
  PLUS   
       :  '+';
  DOT   
       :  '.';
  MUL   
       : '*';
  DOTDOT   
       :  '..';
  AT   
       : '@';
  COMMA  
       : ',';
  PIPE   
       :  '|';
  LESS   
       :  '<';
  MORE_ 
       :  '>';
  EQ
       :  '==' | '=';
  LE   
       :  '<=';
  GE   
       :  '>=';
  COLON   
       :  ':';
  CC   
       :  '::';
  APOS   
       :  '\'';
  QUOT   
       :  '"';
STRING  :  '"' ~'"'* '"'
  |  '\'' ~'\''* '\''
  ;  

Whitespace
  :  (' '|'\t'|'\n'|'\r')+ ->skip
  ;
