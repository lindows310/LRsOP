#if __cplusplus >= 202002L
#define _USE_MATH_DEFINES
#include <iostream>
#include <math.h>
#include <iomanip>
#include <string>	
#include <format>
double func_asm(double x, double y) {
	double result; int error = 0;
	double k1 = 50 / 51.0; double k2 = 2 / 3.0; double k3 = 3 / 2.0;
	int const_2 = 2;
	                             // Задание варианта 50: x^(50/51) + (1/2)^(-2x/3) + sin(2x/3) + e^(sin(x)) + tg(2x/3) - (2/3)sec(2x/3) + ln(2x/3) + 2e^(2x/3) + (3/2)^y (-)
	                             // (-) sqrt(e^(cos(2y/3)) - e^(sin(2y/3))) + e^(cos(y)) + sec(y)
	                             // !!! ЗАМЕЧАНИЕ: ОДЗ функции: x > 0 && y != nPi/2 (n e N) && x != 3nPi/4
	__asm {
		                         //                   ST0                   ST1                   ST2                   ST3                   ST4
		finit                    //                    -                     -                     -                     -                     -
		fldz                     //                    0                     -                     -                     -                     -
		fld1                     //                    1                     0                     -                     -                     -
		fld	st(0)                //                    1                     1                     0                     -                     -
		fld x                    //                    x                     1                     1                     0                     - ! Начало вычисления x^(50/51) - term1
		fcomi st(0), st(3)       // !!! Проверка ОДЗ ln(2x/3)(x > 0)
		jle error_exit           // !!! Переходим, если x <= 0
		fyl2x                    //                 log2(x)                  1                     0                     -                     -
		fld k1                   //                  50/51                log2(x)                  1                     0                     -
		fmulp st(1), st(0)       //              (50/51)log2(x)              1                     0                     -                     -
		fld st(0)                //              (50/51)log2(x)        (50/51)log2(x)              1                     0                     -
		frndint                  //             [(50/51)log2(x)]       (50/51)log2(x)              1                     0                     -
		fsub st(1), st(0)        //             [(50/51)log2(x)]      {(50/51)log2(x)}             1                     0                     -
		fxch st(1)               //             {(50/51)log2(x)}      [(50/51)log2(x)]
		f2xm1                    //           2^{(50/51)log2(x)}-1    [(50/51)log2(x)]             1                     0                     -
		fld1                     //                    1            2^{(50/51)log2(x)}-1     [(50/51)log2(x)]            1                     0
		faddp st(1), st(0)       //            2^{(50/51)log2(x)}     [(50/51)log2(x)]             1                     0                     -
		fscale                   //                x^(50/51)          {(50/51)log2(x)}             1                     0                     -
		fstp st(1)               //                x^(50/51)                 1                     0                     -                     -
		fxch st(1)               //                    1                 x^(50/51)                 0                     -                     - ! Начало вычисления (1/2)^(2x/3) - term2
		fld k2                   //                   2/3                    1                 x^(50/51)                 0                     -
		fld x                    //                    x                    2/3                    1                     1                 x^(50/51)
		fmulp st(1), st(0)       //                  2x/3                    1                 x^(50/51)                 0                     -
		fld st(0)                //                  2x/3                  2x/3                    1                 x^(50/51)                 0
		frndint                  //                 [2x/3]                 2x/3                    1                 x^(50/51)                 0
		fsub st(1), st(0)        //                 [2x/3]                {2x/3}                   1                 x^(50/51)                 0
		fxch st(1)               //                 {2x/3}                [2x/3]                   1                 x^(50/51)                 0
		f2xm1                    //               2^{2x/3}-1              [2x/3]                   1                 x^(50/51)                 0
		fld1                     //                    1                2^{2x/3}-1              [2x/3]                   1                 x^(50/51)
		faddp st(1), st(0)       //                2^{2x/3}               [2x/3]                   1                 x^(50/51)                 0
		fscale                   //                2^(2x/3)               [2x/3]                   1                 x^(50/51)                 0
		fstp st(1)               //                2^(2x/3)                  1                 x^(50/51)                 0                     -
		fstp st(1)               //                2^(2x/3)             x^(50/51)                  0                     -                     -
		faddp st(1), st(0)       //            2^(2x/3)+x^(50/51)            0                     -                     -                     -
		fld k2                   //                   2/3            2^(2x/3)+x^(50/51)            0                     -                     - ! Начало вычисления sin(2x/3) - term3
		fld x                    //                    x                    2/3             2^(2x/3)+x^(50/51)           0                     -
		fmulp st(1), st(0)       //                  2x/3            2^(2x/3)+x^(50/51)            0                     -                     -
		fsin                     //                sin(2x/3)         2^(2x/3)+x^(50/51)            0                     -                     -
		faddp st(1), st(0)       //       sin(2x/3)+2^(2x/3)+x^(50/51)       0                     -                     -                     -
		fldl2e                   //                 log2(e)      sin(2x/3)+2^(2x/3)+x^(50/51)      0                     -                     - ! Начало вычисления e^(sin(x)) - term4
		fld x                    //                    x                  log2(e)       sin(2x/3)+2^(2x/3)+x^(50/51)     0                     -
		fsin                     //                  sin(x)               log2(e)       sin(2x/3)+2^(2x/3)+x^(50/51)     0                     -
		fmulp st(1), st(0)       //              sin(x)*log2(e)  sin(2x/3)+2^(2x/3)+x^(50/51)      0                     -                     -
		fild const_2              //                    2              sin(x)*log2(e)    sin(2x/3)+2^(2x/3)+x^(50/51)     0                     -
		fdivp st(1), st(0)       //            sin(x)*log2(e)/2  sin(2x/3)+2^(2x/3)+x^(50/51)      0                     -                     -
		f2xm1                    //             e^(sin(x)/2)-1   sin(2x/3)+2^(2x/3)+x^(50/51)      0                     -                     -
		fld1                     //                    1              e^(sin(x)/2)-1    sin(2x/3)+2^(2x/3)+x^(50/51)     0                     -
		faddp st(1), st(0)       //               e^(sin(x)/2)   sin(2x/3)+2^(2x/3)+x^(50/51)      0                     -                     -
		fmul st(0), st(0)        //                e^(sin(x))    sin(2x/3)+2^(2x/3)+x^(50/51)      0                     -                     -
		faddp st(1), st(0)       //  e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51)   0                    -                     -                     -
		fld k2                   //                   2/3  e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51)  0                     -                     - ! Началов вычисления (2/3)/cos(2x/3) - term6 (значение в регистре - со знаком "+" - впоследствии вычитается)
		fld st(0)                //                   2/3                   2/3  e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51)  0                     -
		fld x                    //                    x                    2/3                   2/3 e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51)   0
		fmulp st(1), st(0)       //                  2x/3                   2/3  e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51)  0                     -
		fcos                     //                cos(2x/3)                2/3  e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51)  0                     -
		fcomi st(0), st(3)       // !!! Проверка ОДЗ 1/x (в 1/cos(2x/3) = (cos(2x/3))^(-1), cos(2x/3) != 0
		je error_exit            // !!! Переходим, если cos(2x/3) равен нулю
		fdivp st(1), st(0)       //           (2/3)/cos(2x/3) e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51) 0                   -                     -
		                         //
		fsubp st(1), st(0)       // !!! В ST(1) - 1-ый промежуточный результат вычислений (e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51) - (2/3)/cos(2x/3))
		sub esp, 12              // !!! Тип числа в ST(1) - real10, 10-байтовое число расширенной точности с плавающей запятой, выделяем для него в памяти 12 байт (выровнено по 4)
		fstp tbyte ptr[esp]      // !!! Перемещаем с очисткой результат промежуточного вычисления в память
		                         //
		fld k2                   //                   2/3                    0                     -                     -                     - ! Начало вычисления tg(2x/3) - term5
		fld x                    //                    x                    2/3                    0                     -                     -
		fmulp st(1), st(0)       //                  2x/3                    0                     -                     -                     -
		fsincos                  //                sin(2x/3)             cos(2x/3)                 0                     -                     -
		fdivp st(1), st(0)       //                 tg(2x/3)                 0                     -                     -                     -
		fld1                     //                    1                  tg(2x/3)                 0                     -                     - ! начало вычисления ln(2x/3) - term7
		fld k2                   //                   2/3                    1                  tg(2x/3)                 0                     -                     
		fld x                    //                    x                    2/3                    1                  tg(2x/3)                 0                     
		fmulp st(1), st(0)       //                  2x/3                    1                  tg(2x/3)                 0                     -                     
		fyl2x                    //               log2(2x/3)                 1                  tg(2x/3)                 0                     -
		fldl2e                   //                 log2(e)              log2(2x/3)             tg(2x/3)                 0                     -
		fdivp st(1), st(0)       //                ln(2x/3)               tg(2x/3)                 0                     -                     -
		faddp st(1), st(0)       //            ln(2x/3)+tg(2x/3)             0                     -                     -                     -
		fld k2                   //                   2/3             ln(2x/3)+tg(2x/3)            0                     -                     - ! начало вычисления 2e^(2x/3) - term8
		fld x                    //                    x                    2/3             ln(2x/3)+tg(2x/3)            0                     -
		fmulp st(1), st(0)       //                  2x/3             ln(2x/3)+tg(2x/3)            0                     -                     -
		fldl2e                   //                 log2(e)                2x/3             ln(2x/3)+tg(2x/3)            0                     -
		fmulp st(1), st(0)       //             (2x/3)log2(e)         ln(2x/3)+tg(2x/3)            0                     -                     -
		fld st(0)                //             (2x/3)log2(e)          (2x/3)log2(e)        ln(2x/3)+tg(2x/3)            0                     -
		frndint                  //            [(2x/3)log2(e)]         (2x/3)log2(e)        ln(2x/3)+tg(2x/3)            0                     -
		fsub st(1), st(0)        //            [(2x/3)log2(e)]        {(2x/3)log2(e)}       ln(2x/3)+tg(2x/3)            0                     -
		fxch st(1)               //            {(2x/3)log2(e)}        [(2x/3)log2(e)]       ln(2x/3)+tg(2x/3)            0                     -
		f2xm1                    //          2^{(2x/3)log2(e)}-1      [(2x/3)log2(e)]       ln(2x/3)+tg(2x/3)            0                     -
		fld1                     //                    1            2^{(2x/3)log2(e)}-1      [(2x/3)log2(e)]       ln(2x/3)+tg(2x/3)           0
		faddp st(1), st(0)       //           2^{(2x/3)log2(e)}       [(2x/3)log2(e)]       ln(2x/3)+tg(2x/3)            0                     -
		fscale                   //                 e^(2x/3)          [(2x/3)log2(e)]       ln(2x/3)+tg(2x/3)            0                     -
		fstp st(1)               //                 e^(2x/3)         ln(2x/3)+tg(2x/3)             0                     -                     -
		fild const_2             //                    2                 e^(2x/3)           ln(2x/3)+tg(2x/3)            0                     -
		fmulp st(1), st(0)       //                2e^(2x/3)         ln(2x/3)+tg(2x/3)             0                     -                     -
		faddp st(1), st(0)       //          2e^(2x/3)+ln(2x/3)+tg(2x/3)     0                     -                     -                     -
		fldl2e                   //                 log2(e)     2e^(2x/3)+ln(2x/3)+tg(2x/3)        0                     -                     -
		fld k2                   //                   2/3                 log2(e)       2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     - ! начало вычисления -sqrt(e^(cos(2y/3) - e^(sin(2y/3))) - term10
		fld y                    //                    y                    2/3                 log2(e)     2e^(2x/3)+ln(2x/3)+tg(2x/3)        0
		fmulp st(1), st(0)       //                  2y/3                 log2(e)       2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fcos                     //                cos(2y/3)              log2(e)       2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fmulp st(1), st(0)       //             log2(e)cos(2y/3)  2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -                     -
		fild const_2             //                    2              log2(e)cos(2y/3) e^(2x/3)+ln(2x/3)+tg(2x/3)        0                     -
		fdivp st(1), st(0)       //           log2(e)cos(2y/3)/2 2e^(2x/3)+ln(2x/3)+tg(2x/3)       0                     -                     -
		f2xm1                    //           e^(cos(2y/3)/2)-1  2e^(2x/3)+ln(2x/3)+tg(2x/3)       0                     -                     -
		fld1                     //                    1              e^(cos(2y/3)/2)-1  2e^(2x/3)+ln(2x/3)+tg(2x/3)     0                     -
		faddp st(1), st(0)       //             e^(cos(2y/3)/2)  2e^(2x/3)+ln(2x/3)+tg(2x/3)       0                     -                     -
		fmul st(0), st(0)        //               e^(cos(2y/3))  2e^(2x/3)+ln(2x/3)+tg(2x/3)       0                     -                     -
		fld k2                   //                   2/3               e^(cos(2y/3))   2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fld y                    //                    y                    2/3                e^(cos(2y/3)  2e^(2x/3)+ln(2x/3)+tg(2x/3)       0
		fmulp st(1), st(0)       //                  2y/3               e^(cos(2y/3))   2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fsin                     //                sin(2y/3)            e^(cos(2y/3))   2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fldl2e                   //                 log2(e)              sin(2y/3)             e^(cos(2y/3)  2e^(2x/3)+ln(2x/3)+tg(2x/3)       0
		fmulp st(1), st(0)       //            log2(e)sin(2y/3)         e^(cos(2y/3))   2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fild const_2             //                    2              log2(e)sin(2y/3)         e^(cos(2y/3)  2e^(2x/3)+ln(2x/3)+tg(2x/3)       0
		fdivp st(1), st(0)       //           log2(e)sin(2y/3)/2        e^(cos(2y/3))   2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		f2xm1                    //           e^(sin(2y/3)/2)-1         e^(cos(2y/3))   2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fld1                     //                    1             e^(sin(2y/3)/2)-1         e^(cos(2y/3)  2e^(2x/3)+ln(2x/3)+tg(2x/3)       0
		faddp st(1), st(0)       //            e^(sin(2y/3)/2)          e^(cos(2y/3))   2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fmul st(0), st(0)        //             e^(sin(2y/3))           e^(cos(2y/3))   2e^(2x/3)+ln(2x/3)+tg(2x/3)      0                     -
		fsubp st(1), st(0)       //    e^(cos(2y/3))-e^(sin(2y/3)) 2e^(2x/3)+ln(2x/3)+tg(2x/3)     0                     -                     -
		                         //
		fcomi st(0), st(2)       // !!! Проверка ОДЗ корня (e^(cos(2y/3))-e^(sin(2y/3)) > 0)
		jl error_exit            // !!! Переходим, если разность меньше нуля
		                         //
		fsqrt                    // sqrt(e^(cos(2y/3))-e^(sin(2y/3))) 2e^(2x/3)+ln(2x/3)+tg(2x/3)  0                     -                     -
		fchs                     //-sqrt(e^(cos(2y/3))-e^(sin(2y/3))) 2e^(2x/3)+ln(2x/3)+tg(2x/3)  0                     -                     - 
								 //
        faddp st(1), st(0)       // !!! В ST(1) - промежуточный результат вычислений e^(2x/3)+ln(2x/3)+tg(2x/3) - sqrt(e^(cos(2y/3))-e^(sin(2y/3)))
		sub esp, 12              // !!! Тип числа в ST(1) - real10, 10-байтовое число расширенной точности с плавающей запятой, выделяем для него в памяти 12 байт (выравнивание)
		fstp tbyte ptr[esp]      // !!! Перемещаем с очисткой результат промежуточного вычисления в память
								 //
		fld1                     //                    1                    0                      -                     -                     - ! Начало вычисления sec(y) - term12
		fld y                    //                    y                    1                      0                     -                     -
		fcos                     //                  cos(y)                 1                      0                     -                     -
		                         //
		fcomi st(0), st(2)       // !!! Проверка ОДЗ 1/x (в sec(y) = 1/cos(y), cos(y) != 0)
		je error_exit            // !!! Переходим, если cos(y) равен нулю
		       					 //
		fdivp st(1), st(0)       //                 1/cos(y)                0                      -                     -                     -
		fld y                    //                    y                 1/cos(y)                  0                     -                     - ! Начало вычисления e^(cos(y)) - term11
		fcos                     //                  cos(y)              1/cos(y)                  0                     -                     -
		fldl2e                   //                 log2(e)               cos(y)                1/cos(y)                 0                     -
		fmulp st(1), st(0)       //              cos(y)*log2(e)          1/cos(y)                  0                     -                     -
		fild const_2             //                    2              cos(y)*log2(e)            1/cos(y)                 0                     -
		fdivp st(1), st(0)       //             cos(y)*log2(e)/2         1/cos(y)                  0                     -                     -
		f2xm1                    //              e^(cos(y)/2)-1          1/cos(y)                  0                     -                     -
		fld1                     //                    1              e^(cos(y)/2)-1            1/cos(y)                 0                     -
		faddp st(1), st(0)       //               e^(cos(y)/2)           1/cos(y)                  0                     -                     -
		fmul st(0), st(0)        //                e^(cos(y))            1/cos(y)                  0                     -                     -
		faddp st(1), st(0)       //            1/cos(y)+e^(cos(y))          0                      -                     -                     -
		fld1                     //                    1           1/cos(y)+e^(cos(y))             0                     -                     - ! Начало вычисления (3/2)^y - term9
		fld k3                   //                   3/2                   1             1/cos(y)+e^(cos(y))            0                     -                     
		fyl2x                    //                log2(3/2)                1             1/cos(y)+e^(cos(y))            0                     -
		fld y                    //                    y                log2(3/2)         1/cos(y)+e^(cos(y))            0                     -
		fmulp st(1), st(0)       //               y*log2(3/2)      1/cos(y)+e^(cos(y))             0                     -                     -
		fld st(0)                //               y*log2(3/2)         y*log2(3/2)         1/cos(y)+e^(cos(y))            0                     -
		frndint                  //              [y*log2(3/2)]        y*log2(3/2)         1/cos(y)+e^(cos(y))            0                     -
		fsub st(1), st(0)        //              [y*log2(3/2)]       {y*log2(3/2)}        1/cos(y)+e^(cos(y))            0                     -
		fxch st(1)               //              {y*log2(3/2)}       [y*log2(3/2)]        1/cos(y)+e^(cos(y))            0                     -
		f2xm1                    //            2^{y*log2(3/2)}-1     [y*log2(3/1)]        1/cos(y)+e^(cos(y))            0                     -
		fld1                     //                    1           2^{y*log2(3/2)}-1         [y*log2(3/1)]       1/cos(y)+e^(cos(y))           0
		faddp st(1), st(0)       //            2^{y*log2(3/2)}       [y*log2(3/2)]       1/cos(y)+e^(cos(y))             0                     -
		fscale                   //                 (3/2)^y          [y*log2(3/2)]       1/cos(y)+e^(cos(y))             0                     -
		fstp st(1)
		faddp st(1), st(0)       //      1/cos(y)+e^(cos(y))+(3/2)^y        0                      -                     -                     -
								 //
		fld tbyte ptr[esp]       //
		faddp st(1), st(0)       // !!! ST1 = 1/cos(y)+e^(cos(y))+(3/2)^y + e^(2x/3)+ln(2x/3)+tg(2x/3) - sqrt(e^(cos(2y/3))-e^(sin(2y/3)))
		fld tbyte ptr[esp + 12]  //
		faddp st(1), st(0)       // !!! ST1 = 1/cos(y)+e^(cos(y))+(3/2)^y + e^(2x/3)+ln(2x/3)+tg(2x/3) - sqrt(e^(cos(2y/3))-e^(sin(2y/3))) + (e^(sin(x))+in(2x/3)+2^(2x/3)+x^(50/51) - (2/3)/cos(2x/3))
	    add esp, 24
		jmp end

	    error_exit :
		mov error, 1

		end :
		fstp qword ptr[result]
	}
	if (error == 1 || std::isnan(result) || std::isinf(result))
		throw 0;
	return result;
}

double func_cpp(double x, double y) {
	double term1 = pow(x, 50 / 51.0);                                               // term1 = x^(50/51)
	double term2 = pow(2, 2 * x / 3.0);                                             // term2 = (1/2)^(-2x/3)
	double term3 = sin(2 * x / 3.0);                                                // term3 = sin(2x/3)
	double term4 = pow(M_E, sin(x));                                                // term4 = e^(sin(x))
	double term5 = tan(2 * x / 3.0);                                                // term5 = tg(2x/3)
	double term6 = -(2 / 3.0) * 1.0 / cos(2 * x / 3.0);                             // term6 = -(2/3)*(cos(2x/3))^(-1)
	double term7 = log(2 * x / 3.0);                                                // term7 = ln(2x/3)
	double term8 = 2 * pow(M_E, 2 * x / 3.0);                                       // term8 = 2e^(2x/3)
	double term9 = pow(3 / 2.0, y);                                                 // term9 = (3/2)^y
	double term10 = -sqrt(pow(M_E, cos(2 * y / 3.0)) - pow(M_E, sin(2 * y / 3.0))); // term10 = -sqrt(e^(cos(2y/3) - e^(sin(2y/3)))
	double term11 = pow(M_E, cos(y));                                               // term11 = e^(cos(y))
	double term12 = 1 / cos(y);                                                     // term12 = sec(y)

	double result = term1 + term2 + term3 + term4 + term5 + term6 + term7 + term8 + term9 + term10 + term11 + term12;
	if (std::isnan(result) || std::isinf(result))
		throw 0;
	return result;
}

int main()
{
	setlocale(LC_ALL, "Russian");
	std::cout.precision(15);

	printf("Ассемблер. Лабораторная работа № 5. Выполнил студент группы 6101-020302D Абросимов Артём\n");
	printf("========================================================================================");
	printf("\nЗадание, ВАРИАНТ 50: \nx ^ (50 / 51) + (1 / 2) ^ (-2x / 3) + sin(2x / 3) + e ^ (sin(x)) + tg(2x / 3) - (2 / 3)sec(2x / 3) + ln(2x / 3) (+)\n(+) 2e ^ (2x / 3) + (3 / 2) ^ y - sqrt(e^(cos(2y/3)) - e^(sin(2y/3))) + e^(cos(y)) + sec(y)\n");
	printf("========================================================================================");
	double x; double y; double x0; double x1; double y0; double y1;
	double hx; double hy; int num = 1;

	printf("\nВведите начальное значение отрезка значений x0: "); std::cin >> x0;
	printf("Введите конечное значение отрезка значений x1: ");  std::cin >> x1;

	printf("\nВведите начальное значение отрезка значений y0: "); std::cin >> y0;
	printf("Введите конечное значение отрезка значений y1: ");  std::cin >> y1;

	printf("\nВведите величину шага hx: "); std::cin >> hx;
	printf("Введите величину шага hy: "); std::cin >> hy;

	printf("========================================================================================");
	printf("\n(Прочерк - значение неопределенно или +/- бесконечно (переполнение double))\n\n");

	std::cout << "+-------+------------------------------+------------------------------+------------------------------+------------------------------+\n";
	std::cout << std::format("|{:^7}|{:^30}|{:^30}|{:^30}|{:^30}|\n", "№", "x", "y", "f(x,y) asm", "f(x,y) c++");
	std::cout << "+-------+------------------------------+------------------------------+------------------------------+------------------------------+\n";

	for (double i = x0; (hx > 0 ? i <= x1 : i >= x1) ; i += hx)
		for (double j = y0; (hy > 0 ? j <= y1 : j >= y1); j += hy)
		{
			std::cout << std::format("|{:^7}|{:^30}|{:^30}", num, i, j);
			try
			{
				std::cout << std::format("|{:^30}|", func_asm(i, j));
			}
			catch (...)
			{
				std::cout << std::format("|{:^30}|", "-");
			}
			try
			{
				std::cout << std::format("{:^30}|\n", func_cpp(i, j));
			}
			catch (...)
			{
				std::cout << std::format("{:^30}|\n", "-");
			}
			num++;
		}
	std::cout << "+-------+------------------------------+------------------------------+------------------------------+------------------------------+\n";

}
#endif