#include <iostream>
#include <stdio.h>

int calc_asm(int a, int b)
{
    int result;
    __asm {
        mov eax, a
        mov ebx, b
        cmp eax, ebx
        jg a_larger
        jl a_lesser

        mov result, 100  // a = b => X = 100
        jmp exit_label

            a_larger :   // метка, ассоциированная с выражением (2a - b)/a + 12
        mov ecx, a
        imul eax, 2      // EAX = 2*a
        sub eax, b       // EAX = 2*a - b
        cmp ecx, 0
        je div_by_zero
        cdq
        idiv ecx         // EAX = (2*a - b)/a
        add eax, 12      // EAX = (2*a - b)/a + 12
        mov result, eax  // result = EAX
        jmp exit_label

            a_lesser:    // метка, ассоциированная с выражением (4a*a - 255)/(a + b)
        mov ecx, eax
        imul eax         // EAX = a*a
        imul eax, 4      // EAX = 4*a*a
        sub eax, 255     // EAX = 4*a*a - 255
        add ecx, ebx     // ECX = a + b
        cmp ecx, 0
        je div_by_zero
        cdq
        idiv ecx         // EAX = (4*a*a - 255)/(a+b)
        mov result, eax  // result = EAX
        jmp exit_label

    div_by_zero:
    exit_label:
    }
    return result;
}

int calc_c(int a, int b)
{
    if (a > b)
    {
        if (a == 0)
            throw(std::string("Ошибка деления на ноль"));

        return (2 * a - b) / a + 12;
    }
    else if (a == b)
        return 100;
    else
    {
        if (a + b == 0)
            throw(std::string("Ошибка деления на ноль"));

        return (4 * a * a - 255) / (a + b);
    }
}

int main()
{
    setlocale(LC_ALL, "Russian");

    printf("Ассемблер. Лабораторная работа №2. Выполнил студент группы 6101-020302D Абросимов Артём\n");
    printf("Вариант 1. Выражение: \nпри a > b: (2a - b)/a + 12 \nпри a = b: 100 \nпри a < b: (4a*a - 255)/(a + b)\n");
    printf("Последовательно введите значения чисел a, b:\n");

    int a, b;
    printf("a = "); std::cin >> a;
    printf("b = "); std::cin >> b;

    try
    {
        printf("Результат на C++: %d \n", calc_c(a, b));
        printf("Результат на ASM: %d \n", calc_asm(a, b));
    }
    catch (std::string error_message)
    {
        std::cout << error_message << std::endl;
    }
    system("PAUSE");
}
