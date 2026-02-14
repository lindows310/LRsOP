#include <stdio.h>
#include <iostream>

int calc_asm(int a, int b, int c, int d)
{
    // (7*a-40-8*b*b)/(6*c+83/d) - выражение (вариант 1)
    int result = 0;
    __asm {
        mov eax, a
        mov ebx, b
        mov ecx, c

        cmp d, 0;        проверка деления на ноль
        je error_exit
        mov esi, d
        imul ebx, ebx    // EBX = b * b
        shl ebx, 3       // EBX = 8 * b * b
        imul eax, 7      // EAX = 7 * a
        sub eax, 40      // EAX = 7 * a - 40
        sub eax, ebx     // EAX = 7 * a - 40 - 8 * b * b
        push eax         // ^ в стеке
        imul ecx, 6      // ECX = 6 * c
        mov eax, 83      // EAX = 83
        cdq              // расширение знаком
        idiv esi         // EAX = 83 / d
        add ecx, eax     // ECX = 6 * c + 83 / d
        cmp ecx, 0       // проврека деления на ноль
        je error_exit
        pop eax          // EAX = 7 * a - 40 - 8 * b * b
        cdq              // расширение знаком
        idiv ecx         // EAX = (7 * a - 40 - 8 * b * b) / (6 * c + 83 / d)
        mov result, eax  // result = EAX
        jmp end_asm

        error_exit :     // метка проверки нуля. Аварийно заканчивает работу метода, если в результате сравнения был установлен ZF.
        mov result, -1

        end_asm :        // метка выхода из метода.
    }
    return result;
}

int calcC(int a, int b, int c, int d)
{
    if (d != 0 && (6 * c + 83 / d) != 0)
        return ((7 * a - 40 - 8 * b * b) / (6 * c + 83 / d));
    else
    {
        printf("Ошибка деления на ноль. \n");
        return -1;
    }
}

void main()
{
    setlocale(LC_ALL, "ru");
    printf("Выполнил студент группы 6101-020302D Абросимов Артём. \nВариант 1\nВыражение: (7*a-40-8*b*b)/(6*c+83/d)\n");

    int a, b, c, d;
    std::cout << "a = " << std::endl;
    std::cin >> a;
    std::cout << "b = " << std::endl;
    std::cin >> b;
    std::cout << "c = " << std::endl;
    std::cin >> c;
    std::cout << "d = " << std::endl;
    std::cin >> d;

    printf("\n");
    int resAsm = calc_asm(a, b, c, d);
    int resC = calcC(a, b, c, d);
    printf("Ответ на asm: %d \n", resAsm);
    printf("Ответ на C++: %d \n", resC);
    system("PAUSE");
}