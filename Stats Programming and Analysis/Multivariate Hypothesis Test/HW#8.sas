data carbody;
infile datalines delimiter=",";
input x1 x2 x3 x4 x5 x6;
datalines; 
-0.12,0.36,0.4,0.25,1.37,-0.13
-0.6,-0.35,0.04,-0.28,-0.25,-0.15
-0.13,0.05,0.84,0.61,1.45,0.25
-0.46,-0.37,0.3,0,-0.12,-0.25
-0.46,-0.24,0.37,0.13,0.78,-0.15
-0.46,-0.16,0.07,0.1,1.15,-0.18
-0.46,-0.24,0.13,0.02,0.26,-0.2
-0.13,0.05,-0.01,0.09,-0.15,-0.18
-0.31,-0.16,-0.2,0.23,0.65,0.15
-0.37,-0.24,0.37,0.21,1.15,0.05
-1.08,-0.83,-0.81,0.05,0.21,0
-0.42,-0.3,0.37,-0.58,0,-0.45
-0.31,0.1,-0.24,0.24,0.65,0.35
-0.14,0.06,0.18,-0.5,1.25,0.05
-0.61,-0.35,-0.24,0.75,0.15,-0.2
-0.61,-0.3,-0.2,-0.21,-0.5,-0.25
-0.84,-0.35,-0.14,-0.22,1.65,-0.05
-0.96,-0.85,0.19,-0.18,1,-0.08
-0.9,-0.34,-0.78,-0.15,0.25,0.25
-0.46,0.36,0.24,-0.58,0.15,0.25
-0.9,-0.59,0.13,0.13,0.6,-0.08
-0.61,-0.5,-0.34,-0.58,0.95,-0.08
-0.61,-0.2,-0.58,-0.2,1.1,0
-0.46,-0.3,-0.1,-0.1,0.75,-0.1
-0.6,-0.35,-0.45,0.37,1.18,-0.3
-0.6,-0.36,-0.34,-0.11,1.68,-0.32
-0.31,0.35,-0.45,-0.1,1,-0.25
-0.6,-0.25,-0.42,0.28,0.75,0.1
-0.31,0.25,-0.34,-0.24,0.65,0.1
-0.36,-0.16,0.15,-0.38,1.18,-0.1
run;

proc iml;
  start hotel;
    mu0={0, 0, 0, 0, 0, 0};
    *j function is used to create a matrix. j (#_rows, #_columns, values);
    one=j(nrow(x),1,1); 	*creates the 1_n column vector, with the #_rows comming from our random column vector;
    * i function creates the identity matrix. i (dimension);
    ident=i(nrow(x)); 		*creates the identity matrix having the dimension of out random column vector;
    ybar=x`*one/nrow(x); 	*creates the sample mean vector;
    *creates the sample covariance matrix. S = X'HX/(n-1), where H is the centering matrix H = I_n-(1_n*1_n')/n;
    s=x`*(ident-one*one`/nrow(x))*x/(nrow(x)-1.0); 
    print mu0 ybar;
    print s;
    *calculate Hoteling T^2 statistic, T^2 = n(Xbar - mu_0)'S^-1(Xbar - mu_0);
    t2=nrow(x)*(ybar-mu0)`*inv(s)*(ybar-mu0);
    *Transform Hoteling T^2 to the F statistic, with degrees of freedom p = col(x) and n-p = row(x)-col(x);
    f=(nrow(x)-ncol(x))*t2/ncol(x)/(nrow(x)-1);
    df1=ncol(x);
    df2=nrow(x)-ncol(x);
    *Gets the p-value. using probf that finds the probability of an F distribution with degrees of freedom df1 and df2;
    p=1-probf(f,df1,df2);
    print t2 f df1 df2 p;
  finish;
  use carbody;
  read all var{x1 x2 x3 x4 x5 x6} into x;
  run hotel;