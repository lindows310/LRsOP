def recFunc(n):
    if (n < 2):
       return n
    if (n >= 2 and n % 2 == 0):
       return recFunc(n // 2) + 1
    if (n >= 2 and n % 2 != 0):
       return recFunc(3 * n + 1) + 1

def recFunc2(minNum, maxNum, num):
    if (num > 0):
        temp = num
        temp %= 10
        if (temp > maxNum):
            maxNum = temp
        if (temp < minNum):
            minNum = temp
        num //= 10
        return recFunc2(minNum, maxNum, num)
    else:
        return (minNum - maxNum)

def Counter():
    c = 0
    for n in range(1, 101):
        if (recFunc(n) > 100):
            c += 1
    return c

def userInput(inp):
    maxNum = 0
    minNum = 9
    num = recFunc(inp)
    print(recFunc(inp))
    return(recFunc2(minNum, maxNum, num))

print("Лабораторная работа No 4")
print("Вариант No 1. Выполнил студент группы 6101-020302D Абросимов Артём Олегович")
print("Задание:")
print("Алгоритм вычисления функции F(n), где n - целое число, задан следующими отношениями:")
print("F(n) = n при n < 2,")
print("F(n // 2) + 1, когда n >= 2 и чётное,")
print("F(3n + 1) + 1, когда n >= 2 и нечётное. ")
print("Написать программу, которая вычисляет:")
print("1. Количество значений n на отрезке [1; 100], для которых F(n) определено и больше 100.")
print("2. Разность между минимальной и максимальной цифрами результата вычисления F(x), где x - число, заданное пользователем")
print("")

count = Counter()
print("Ответ 1: количество значений n на отрезке [1; 100], для которых выполняется условие первого задания: ", count)
inp = int(input("Введите x: "))
print("Ответ 2: разность между минимальной и максимальной цифрами результата вычисления F(x): ", userInput(inp))
