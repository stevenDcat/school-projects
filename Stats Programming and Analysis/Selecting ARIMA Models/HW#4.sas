/* Name: Roderick Quiel */
/* Course: STA 4853 */
/* Assignment: Homework#3 */
/* Date: 04/20/2022 */

/*Problem#1*/

/*Data Step*/
filename Repair "~/my_shared_file_links/huffer/repair.txt";
data data1;
time=_n_;
infile Repair;
input x;
y=log(x);
run;

/*Part 1a. */
proc arima data=data1;
identify var=x nlag=24;
identify var=y(1) nlag=24;
identify var=y(1,12) nlag=24;
run;

proc arima data=data1 plots=all;
identify var=y(1,12) nlag=24 noprint;
estimate q=(1)(12) method=ml;/*ARIMA(0,1,1)(0,1,1)_12*/
run;

Title "Model ARIMA(0,1,1)(0,1,1)_12 for Repair series";
proc arima data=data1;
identify var=y(1,12) nlag=24;
estimate q=(1)(12) noconstant method=ml; /*ARIMA(0,1,1)(0,1,1)_12 NOCONSTANT*/
forecast out=resids id=time lead=0 noprint;
run;


title "Forecast v.s Residuals";
proc sgplot data=resids;
loess x=forecast y=residual / clm;
run;

title "Time series of residuals";
proc sgplot data=resids;
loess x=time y=residual / clm;
run;

/* Part 1b. */
title "Model without seasonal term";
proc arima data=data1;
identify var=y(1) nlag=24 noprint;
estimate q=1 noconstant method=ml;
forecast out=resids id=time lead=0 noprint;
run;

title "Forecast v.s Residuals";
proc sgplot data=resids;
loess x=forecast y=residual / clm;
run;

title "Time series of residuals";
proc sgplot data=resids;
loess x=time y=residual / clm;
run;

/*Part 1c. */

proc arima data=data1;
identify var=y(1,12) nlag=24 noprint;
estimate q=(1)(12) noconstant method=ml noprint; /*ARIMA(0,1,1)(0,1,1)_12 NOCONSTANT*/
forecast out=resids lead=36;
run;


proc arima data=data1;
identify var=y(1,12) nlag=24 noprint;
estimate q=(1)(12) method=ml noprint; /*ARIMA(0,1,1)(0,1,1)_12 NOCONSTANT*/
forecast out=resids lead=36;
run;





/*Problem #2*/

/*Data Step*/
filename Bus "~/my_shared_file_links/huffer/bus.txt";
data data2;
time=_n_;
infile Bus;
input x;
y=log(x);
run;

/*Part 2a. */
proc arima data=data2;
identify var=x nlag=24;
identify var=x(1) nlag=24;
identify var=y(12) nlag=24;
run;

Title "Model ARIMA(2,0,0)(0,1,1)_12 for Bus series";
proc arima data=data2;
identify var=y(12) nlag=24 noprint;
estimate p=(1,2)(12) method=ml;
forecast out=resids id=time lead=0 noprint;
run;


title "Forecast v.s Residuals";
proc sgplot data=resids;
loess x=forecast y=residual / clm;
run;

title "Time series of residuals";
proc sgplot data=resids;
loess x=time y=residual / clm;
run;

/*Part 2b. */
proc arima data=data2;
identify var=y(12) nlag=24 noprint;
estimate p=(1,2)(12) method=ml noprint; /*ARIMA(0,1,1)(0,1,1)_12 NOCONSTANT*/
forecast out=resids lead=36 noprint;
run;

data original;
set resids;
keep osforecast osL95 osU95;
osforecast=exp(forecast);
osL95=exp(L95);
osU95=exp(U95);
run;

title "Future 36 Forecasts";
proc print data=original (firstobs=137);
run;




/*Problem#3*/

/*Data Step*/
filename Eureka "~/my_shared_file_links/huffer/eureka.txt";
data data3;
infile Eureka;
input x;
run;


proc arima data=data3;
identify var=x;
run;

data test;
tpi=2*arcos(-1);  
drop tpi;
do time=1 to nobs+36;
if time <= nobs then set data3 nobs=nobs;
else x=. ;  
xsin1=sin(tpi*time/12);
xcos1=cos(tpi*time/12);
xsin2=sin(2*tpi*time/12);
xcos2=cos(2*tpi*time/12);
xsin3=sin(3*tpi*time/12);
xcos3=cos(3*tpi*time/12);
xsin4=sin(4*tpi*time/12);
xcos4=cos(4*tpi*time/12);
xsin5=sin(5*tpi*time/12);
xcos5=cos(5*tpi*time/12);
xcos6=cos(6*tpi*time/12);
output;
end;
run;

proc arima data=test;
identify var=x crosscor=(xsin1 xcos1 xsin2 xcos2
         xsin3 xcos3 xsin4 xcos4 xsin5 xcos5 xcos6);
estimate input=(xsin1 xcos1 xsin2 xcos2
         xsin3 xcos3 xsin4 xcos4 xsin5 xcos5 xcos6) method=ml;
estimate q=1 input=(xsin1 xcos1 xsin2 xcos2
         xsin3 xcos3 xsin4 xcos4 xsin5 xcos5 xcos6) method=ml;
quit;

/*Model for series */
proc arima data=test;
identify var=x crosscor=(xsin1 xcos1 xcos2 xcos3) noprint;
estimate q=1 input=(xsin1 xcos1 xcos2 xcos3) method=ml;
forecast lead=0 id=time out=resids noprint;
quit;

title "Forecast v.s Residuals";
proc sgplot data=resids;
loess x=forecast y=residual / clm;
run;

title "Time series of residuals";
proc sgplot data=resids;
loess x=time y=residual / clm;
run;



/*Part 3b. */
proc arima data=test;
identify var=x crosscor=(xsin1 xcos1 xcos2 xcos3) noprint;
estimate q=1 input=(xsin1 xcos1 xcos2 xcos3) noprint method=ml;
forecast lead=36 out=resids;
quit;














