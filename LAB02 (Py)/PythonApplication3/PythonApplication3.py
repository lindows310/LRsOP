from datetime import date

nums = "0123456789"
alphabet = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю"

def Info():
    print("Лабораторная работа No 4")
    print("Вариант No 1. Выполнил студент группы 6101-020302D Абросимов Артём Олегович")
    print("Условие:")
    print("В исходном текстовом файле хранится информация о книгах: шифр, фамилия автора, назание книги, издательство, год издания.")
    print("Задание:")
    print("В результирующий файл 1 переписать информацию о книгах, выпущенных заданным издательством.")
    print("Проверить, есть ли в исходных данных книги 19nn года издания, если таких книг нет, то в результирующий файл 2 выписать список авторов,")
    print("чье имя начинается на заданную букву.")
    print("")

def Menu():
    print("Выберите пункт меню:")
    print("(1) Записать в текстовый файл информацию о книгах")
    print("(2) Записать в текстовый файл информацию о книгах, выпущенных выбраным издательством")
    print("(3) Проверить наличие в исходном файле книг, выпущенных в 19nn году.")
    print("(любой символ) Выйти из программы")

def tryParse(value):
    try:
        int(value)
        return 1
    except:
        return 0

def FileExists(filename):
    isExists = 1
    try: 
        open(filename + '.txt')
    except: 
        isExists = 0
    return isExists

def dataCheck(parts):
    if (len(parts) != 5):
        return 0
    else:
         dateCheck = parts[4].split(".")
         authorCheck = parts[1].split("-")

         if (len(dateCheck[0]) != 2 or len(dateCheck[1]) != 2 or len(dateCheck[2]) != 4):
             return 0

         if (len(parts[0]) == 13):
            for i in range(len(parts[0])):
               if (parts[0][i] not in nums):
                   return 0
         else:
            return 0

         for i in range(len(authorCheck)):
            for j in range(len(authorCheck[i])):
               if (authorCheck[i][j] not in alphabet and not "-"):
                   return 0

         for i in range(len(dateCheck)):
            if (tryParse(dateCheck[i]) == 0):
                return 0
    return 1

def toDict(f, dictKey):
    dictionary = { }
    for line in f:
        lineSplit = line.split(' ')
        if (len(lineSplit) < 6):
           if (dictKey == "Publisher"):
               publisher = lineSplit[3]
               bookTitle = lineSplit[2]
               if (publisher not in dictionary):
                   dictionary[publisher] = { }
               if (bookTitle not in dictionary[publisher]):
                   dictionary[publisher][bookTitle] = []
               dictionary[publisher][bookTitle].append(lineSplit)

           elif (dictKey == "Date"):
               date = lineSplit[4]
               bookTitle = lineSplit[2]
               if (date not in dictionary):
                   dictionary[date] = { }
               if (bookTitle not in dictionary[date]):
                   dictionary[date][bookTitle] = []
               dictionary[date][bookTitle].append(lineSplit)
           else:
               print("Ошибка. В коде программы введен неверный параметр")
        else:
            print("Введены некорректные данные")

    return dictionary

def FileEd():
    f = None
    filename = input("Введите название редактируемого файла: ")
    if (FileExists(filename) == 1):
        print("Файл уже существует. Введите ""1"", чтобы перезаписать его")
        if (input() == "1"):
            f = open(filename + '.txt', "w", encoding="utf-8")
        else:
            print("Перезапись отменена. Выход из раздела...")
    else:
        f = open(filename + '.txt', "w", encoding="utf-8")

    if f != "":
        linesC = 0
        linesToAdd = int(input("Введите количество книг, которые хотите добавить в файл: "))
        print("Запишите через пробел информацию о книге в следующем порядке: шифр, имя автора, название книги, издательство, год издания. Образец заполнения:")
        print("[0000000000000] [Имя-Фамилия] [Название-Книги] [Издательство] [ДД.ММ.ГГГГ]")
        while (linesToAdd > 0):
            userInp = input()
            parts = userInp.split(" ")
            if (dataCheck(parts) == 1):
                f.write(str(linesC) + ". " + userInp + "\n")
            else:
                print("Данные введены некорректно.")
            linesToAdd -= 1
            linesC += 1

def Task1():
    w = None
    fileReadName = input("Введите имя файла на чтение: ")
    if (FileExists(fileReadName) == 1):
        with open(fileReadName + '.txt', "r", encoding="utf-8") as fileRead:
            fileWriteName = input("Введите имя файла на запись: ")
            if (FileExists(fileWriteName) == 1):
                print("Файл уже существует. Введите ""1"", чтобы перезаписать его")
                if (input() != "1"):
                    print("Перезапись отменена. Выход из раздела...")
                    return
                else:
                    w = open(fileWriteName + '.txt', "w", encoding="utf-8")
            else:
                w = open(fileWriteName + '.txt', "w", encoding="utf-8")

            publisherDict = toDict(fileRead, "Publisher")
            publisher = input("Введите название издательства: ")

            linesC = 0
            if (publisher in publisherDict):
                bad_books = []
                bad = False

                linesC = 1
                for bookTitle in publisherDict[publisher]:
                    for book in publisherDict[publisher][bookTitle]:
                        if dataCheck(book) == 0:
                           w.write(str(linesC) + ". " + ' '.join(book).strip() + "\n")
                           linesC += 1
                        else:
                           bad_books.append(book)
                           bad = True

                if bad and input("Обнаружены повреждённые данные. Ввести '1', чтобы записать их в файл: ") == "1":
                   for book in bad_books:
                       w.write(str(linesC) + ". " + ' '.join(book).strip())
                       print(str(linesC) + ". ")
                       for i in range(len(book)):
                           print(book[i])
                       print("\n")
                       linesC += 1
                else:
                    print("Поврежденные данные не будут записаны в файл.")
            else:
                print("Ошибка. В файле нет выбранного издательства.")
    else:
        print("Ошибка. Файла с введенным именем не существует. Выход из раздела...")

def Task2():
    w = None
    fileReadName = input("Введите имя файла на чтение: ")
    if FileExists(fileReadName) != 1:
        print("Ошибка. Файла с введенным именем не существует. Выход из раздела...")
        return

    with open(fileReadName + '.txt', "r", encoding="utf-8") as fileRead:
        fileWriteName = input("Введите имя файла на запись: ")
        if FileExists(fileWriteName) == 1:
            print("Файл уже существует. Введите '1', чтобы перезаписать его")
            if input() != "1":
                print("Перезапись отменена. Выход из раздела...")
                return

        w = open(fileWriteName + '.txt', "w", encoding="utf-8")
        dateDict = toDict(fileRead, "Date")
        count19nn = 0
        bad_books = []

        for date in dateDict:
            test = date[6:10:]
            if (tryParse(date[6:10:]) == 1) and (1899 < int(date[6:10:]) < 2000):
                count_19nn += 1
            for bookTitle in dateDict[date]:
                for book in dateDict[date][bookTitle]:
                    if not dataCheck(book):
                        bad_books.append(book)

        if count19nn > 0:
            print(f"Найдено книг, выпущенных в 19nn-ых годах: {count19nn}")
        else:
            letter = input("В исходном файле не найдены книги 19nn года. Введите букву фамилии авторов: ")
            for date in dateDict:
                for bookTitle in dateDict[date]:
                    for book in dateDict[date][bookTitle]:
                        if dataCheck(book):
                            author = book[1].split('-')
                            if author[1][0].upper() == letter.upper():
                                w.write(author[1] + "\n")
        w.close()
Info()

run = 1
while (run == 1):
    Menu()

    userInp = input("Введите опцию: ")
    if (userInp == "1"):
        FileEd()
    elif (userInp == "2"):
        Task1()
    elif (userInp == "3"):
        Task2()
    else:
        run = 0
        print("Вы вышли из программы.")
    print("")
