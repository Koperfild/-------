// Пняву1.cpp: определяет точку входа для консольного приложения.
//
#include "stdafx.h"
#include <stdio.h>
#include <string.h>
#include <stdlib.h>//dl9 exit
//#include "iostream.h"
#define M 50//количество пустых строк в очистке экрана, длина полей структуры
#include <locale.h>
#include <windows.h>


struct Record{
	char str1[M];
	char str2[M];
	char str3[M];
} *p;


void input1();
void input2();
void clrscrn();
FILE *openFile(char *filename,char *mode);
int IsAplha(char c);//Аналог isaplha но с учётом русских букв. 1 если буква, 0-иначе
void SkipInput();//Пропуск небукв при вводе.
void SkipInputIsAlnum();

int _tmain(int argc, _TCHAR* argv[])
{
	setlocale(LC_ALL, "");
	p=(struct Record *)malloc(sizeof(struct Record));
    char c='0';
	while (c!='3'){
		clrscrn();
        printf("This program let you add cards of ports and ships\n");
		printf("To add ship card press 1\nIf you want to add port card press 2\n");
		printf("To close program press 3\n");

		//fflush(stdin);
		//getchar();//2 потому что 1 съедается \n
		c=getchar();

		if (c =='1'){//Kod 1
			input1();
		}
		if (c=='2'){//Kod 2
			input2();
		}
	}
	delete[] p;
	return 0;    
}

FILE *openFile(char *filename,char *mode){//в fopen сделано через указатели.filename-имя создаваемого файла,mode - тип открытия файла- wb и т.д.
	errno_t err;
	FILE *f;
	err=fopen_s(&f, filename, "r");//Возвращает 0 в случае успеха
		
	int c;
    if (err!=NULL && mode=="r"){
		printf("Error opening %s file",filename);
		getchar();
		exit (-1);
	}
	if (err==NULL && *mode=='w'){
		fclose(f);
		printf("The file %s already exists\nPress 1 to rewrite it\nPress 2 to cancel\n",filename);
		do{
			scanf("%d",&c);
			if (c==1){
				fopen_s(&f,filename,mode);
				return f;
			}
			else if (c==2){
				f=NULL;
			}
		}while(c!=2);
	}else if (err!=NULL && *mode=='w'){
		fopen_s(&f, filename, mode);
		return f;
	}else if (err==NULL && mode=="ab"){
		fopen_s(&f, filename, mode);
		return f;
	}
	
	return f;
}

void clrscrn(){
    for (int i=0;i<50;++i){//o4istka ekrana
		printf("\n");
    }
}

void input1(){
    FILE *f;
    f=openFile("ships.dat","ab");
	char c='0';
	while(c!='3'){
	    clrscrn();
	    printf("All info will be put to \"ships.dat\"\n\n\n");
		printf("You are filling information about ship\n\n\n");
		printf("Fill-in ship name\n");// Vstavit proverki
		fflush(stdin);
		SkipInputIsAlnum();
		gets(p->str1);
		if (strlen(p->str1)==0){
			strcpy(p->str1,"No info");
		}
		printf("Fill-in port of departure\n");
		SkipInputIsAlnum();
		gets(p->str2);
		if (strlen(p->str2)==0){
			strcpy(p->str2,"No info");
		}
		printf("Fill-in arrival port\n");
		SkipInputIsAlnum();
		gets(p->str3);
		if (strlen(p->str3)==0){
			strcpy(p->str3,"No info");
		}
		while (strcmp(p->str2,p->str3)==0){
			printf("Inputed ports must be different. Please input 2nd port again.\n\n");
			gets(p->str3);
		}
		fwrite (p, sizeof(struct Record), 1, f);
		clrscrn();
		printf("Return to main menu press 3.\n\nTo input one more ship card press any key\n\n");
		c=getchar();//potom peredelat na s4itivanie stroki. I esli strlen > 1 to incorrect input. bez fflush
	}
	fclose(f);
}

void input2(){
	//printf("All info will be put to \"ports.dat\"\n\n\n");
	FILE *f;
	//f=openFile("ports.dat","ab");
	f = fopen("ports.dat", "ab");
	char c='0';
	while(c!='3'){
	    clrscrn();
	    printf("All info will be put to \"ports.dat\"\n\n\n");
		printf("You are filling information about port\n\n\n");

		printf("Fill-in port name\n");// Vstavit proverki
		fflush(stdin);
		SkipInputIsAlnum();
		gets(p->str1);
		printf("Fill-in its country\n");
		int flag1=0;
		while (!flag1){
			char c;
			SkipInput();			
			gets(p->str2);
			//printf("%s\n", p->str2);
			char *g=p->str2;//Для удобства чтоб дальше не писать p->str2
			OemToChar(p->str2, p->str2);
			//printf("%s", p->str2);
			while (*g!='\0'){
				if (IsAlpha(*g)||(strchr("- ",*g))){
					++g;
				}
				else{
					printf("Incorrect input. Try again\n");
					break;
				}
			}
			if (*g=='\0'){
				flag1=1;
			}
		}
		printf("Fill-in its sea\n");
		SkipInput();
		gets(p->str3);

		fwrite (p, sizeof(struct Record), 1, f);
		clrscrn();
		printf("Return to main menu press 3.\nTo input one more port card press any key\n");
		scanf("%c",&c);//pravit na stroky
	}
	fclose(f);
	clrscrn();
}
int IsAlpha(char c){//Аналог isaplha но с учётом русских букв. 1 если буква, 0-иначе
	if ((c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')){
		return 1;
	}
	else return 0;
}
void SkipInput(){
	char c;
	while (!IsAlpha(c = getc(stdin)));
}
void SkipInputIsAlnum(){
	char c;
	while (!IsAlpha(c = getc(stdin)) || !isalnum(c));
}

char *NameCheck(char *Title,void (*skip)(),...){// возвращает указатель на введённую строку//Вводится Title - например "введите порт", функцияпропуска (skipInput) и указатели на функции проверки ввода
	printf("%s\n",Title);
	char c[M];//Строка для хранения названия
	char *p = Title+sizeof(Title)+sizeof(skip);//Указатель на следующий после *Title в строке параметров функции NameCheck
	//p += sizeof(Title);
	int (*fp)(char *c) = (int (*))p;//указатель на функцию. Можно без p написать int(*fp)(char *c) = (int)(Title+sizeof(Title));
	int flag1 = 1;
	while (!flag1){	
		skip();//Пропуск пробелов до слова.
		gets(c);
		//printf("%s\n", p->str2);
		//char *g = s;//Для удобства чтоб дальше не писать p->str2
		OemToChar(c, c);
		//printf("%s", p->str2);
		for (int i=0; fp[i] != NULL; ++i);
		while (*c != '\0'){
			for ()//Проверка каждого условия-функции 
			if (IsAlpha(*g) || (strchr("- ", *c))){
				++c;
			}
			else{
				printf("Incorrect input. Try again\n");
				break;
			}
		}
		if (*c == '\0'){
			flag1 = 1;
		}
	}
}