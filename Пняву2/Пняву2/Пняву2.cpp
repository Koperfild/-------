// Пняву2.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "stdio.h"
#include "string.h"
#include "stdlib.h"//dl9 exit
//#include "iostream.h"
#define M 50//Dlina strok v structure

struct Record{//shipname,port1,port2
	char str1[M];
	char str2[M];
	char str3[M];
} *tmp, *search;//search dl9 sravneni9 s tmp. Vna4ale zapoln9ets9 tolko 1-oe pole


struct Result{
	char Shipname[M];
	struct Record port1;
	struct Record port2;
} *Res;

void ShipNameSearch();
void OutputToFile();
void clrscrn();
FILE *openFile(char *filename, char *mode);

int _tmain(int argc, _TCHAR* argv[])
{
	tmp = new struct Record;
	search = new struct Record;
	Res = new struct Result;

	char c = 0;//vprincipe mojno i int
	int flag = 0;
	while (c != '3'){
		clrscrn();
		printf("This program looks for information about ships\n");
		printf("Press 1 if you want to find specific ship info\n");
		printf("Press 2 to output all data to file\"Shipsoutput.dat\"\n");
		printf("To close program press 3\n");

		fflush(stdin);
		scanf("%c", &c);
		if (c == '1'){//Kod 1
			ShipNameSearch();
		}
		if (c == '2'){//Kod 2
			OutputToFile();
		}
	}
	delete[] tmp;
	delete[] search;
	delete[] Res;
	
	return 0;
}

void clrscrn(){
	for (int i = 0; i<50; ++i){//o4istka ekrana
		printf("\n");
	}
}

FILE *openFile(char *filename, char *mode){//в fopen сделано через указатели.filename-имя создаваемого файла,mode - тип открытия файла- wb и т.д.

	FILE *f;
	f = fopen(filename, "r");//Возвращает 0 в случае успеха

	int c;
	if (f == NULL && mode == "r"){
		printf("Error opening %s file", filename);
		getchar();
		exit(-1);
	}
	if (f != NULL && *mode == 'w'){
		fclose(f);
		printf("The file %s already exists\nPress 1 to rewrite it\nPress 2 to cancel\n", filename);
		do{
			scanf("%d", &c);
			if (c == 1){
				f = fopen(filename, mode);
				return f;
			}
			else if (c == 2){
				f = NULL;
			}
		} while (c != 2);
	}
	else if (f == NULL && *mode == 'w'){
		f = fopen(filename, mode);
		return f;
	}
	else if (f != NULL && mode == "ab"){
		f = fopen(filename, mode);
		return f;
	}

	return f;
}

void ShipNameSearch(){
	FILE *f1, *f2;
	char c = 'y';
	f1 = openFile("ships.dat", "r");
	f2 = openFile("ports.dat", "r");
	clrscrn();
	while (c == 'Y' || c == 'y'){
		fseek(f1, 0, SEEK_SET);//4tobi posle kajdogo poiska v sly4ae eshe 1-ogo vozvrashalis na na4alo failov
		fseek(f2, 0, SEEK_SET);
		do{
			printf("Enter name of ship to find\n");
			scanf("%s", search->str1);
		} while (!strlen(search->str1));
		strcpy(Res->Shipname, search->str1);
		printf("Search for ship %s info\n", search->str1);

		int flag = 0, flag1 = 0, flag2 = 0;//mojno flag=flag1=flag2=0?

		while (fread(tmp, sizeof(*tmp), 1, f1) == 1 && !flag){//mojno sdelat while (fread(...)==1 && strcmp(...)!=0) no nyjno (fread(...))
			if (strcmp(search->str1, tmp->str1) == 0){//sravnenie iskomogo i pro4itannogo pol9
				flag = 1;//nashli boatname kotorii ishem
				*search = *tmp;
				while (fread(tmp, sizeof(*tmp), 1, f2) == 1 && !(flag1 && flag2)){

					if (!flag1 && strcmp(search->str2, tmp->str1) == 0){//Proverka po 2-omy polu.
						Res->port1 = *tmp;
						flag1 = 1;//nashli port1
					}

					if (!flag2 && strcmp(search->str3, tmp->str1) == 0){//proverka po 3-emy polu (2-omy porty)
						Res->port2 = *tmp;
						flag2 = 1;//nashli port2
					}
				}

			}

		}
		if (!flag1){
			strcpy(Res->port1.str2, "No info");
			strcpy(Res->port1.str3, "No info");
		}
		if (!flag2){
			strcpy(Res->port2.str2, "No info");
			strcpy(Res->port2.str3, "No info");
		}
		clrscrn();
		if (!flag){
			printf("No one record with name %s was found\n\n", search->str1);
		}
		else {
			printf("Ship: %s\ndepart from country:%s\nSea:%s\n\n to\n\nCountry:%s\nSea:%s\n", Res->Shipname /*Res->Shipname ne podxodit t.k. nelz9 Res->Shipname=search->str1*/, Res->port1.str2, Res->port1.str3, Res->port2.str2, Res->port2.str3);
			printf("\n\n\n");
		}
		printf("To search again press Y, otherwise any other key\n");
		fflush(stdin);
		scanf("%c", &c);
		fflush(stdin);//ina4e pri vvode stroki sledyushii cikl pervii simvol idet v c a ostalnie v shipname v na4ale cikla (search->str1)
	}
	fclose(f1);
	fclose(f2);
	clrscrn();
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void OutputToFile(){//vivod nyjno delat f textovii fail
	FILE *f1, *f2, *fout;
	int c = 0;
	//int flag1=0, flag2=0;
	clrscrn();
	f1 = openFile("ships.dat", "rb");
	f2 = openFile("ports.dat", "rb");
	if (fout = fopen("Shipsoutput.txt", "r")){//Проверка существования файла вывода данных и выбор действия
		fclose(fout);
		do{
			printf("File Shipsouput.txt already exists. Do you want to rewrite or to supplement it?\nTo rewrite press 1\nTo supplement press 2\n");
			scanf("%d", &c);
			if (c == 1){
				fout = openFile("Shipsoutput.txt", "w");
			}
			if (c == 2){
				fout = openFile("Shipsoutput.txt", "a");
			}
		} while (c != 1 && c != 2);
	}
	else{
		fout = openFile("Shipsoutput.txt", "w");
	}
	/*fseek (f1, 0, SEEK_SET);//yto4nit nyjno li vozvrashat pointer na na4alo faila ili fopen delaet eto sama
	fseek (f2, 0, SEEK_SET);*/
	while (fread(search, sizeof(*tmp), 1, f1) == 1){
		int flag1 = 0, flag2 = 0;
		strcpy(Res->Shipname, search->str1);/*Res->Shipname=search->str1;*/
		while (fread(tmp, sizeof(*tmp), 1, f2) == 1 && !(flag1 && flag2)){

			if (!flag1 && strcmp(search->str2, tmp->str1) == 0){
				Res->port1 = *tmp;
				flag1 = 1;
			}

			if (!flag2 && strcmp(search->str3, tmp->str1) == 0){
				Res->port2 = *tmp;
				flag2 = 1;
			}
		}
		if (!flag1){
			strcpy(Res->port1.str2, "No info");
			strcpy(Res->port1.str3, "No info");
		}
		if (!flag2){
			strcpy(Res->port2.str2, "No info");
			strcpy(Res->port2.str3, "No info");
		}
		fseek(f2, 0, SEEK_SET);//posle kajdogo proxoda vozvrashaems9 v na4alo faila
		fprintf(fout, "Ship: %s\ndepart from\n\nCountry:%s\nSea:%s\n\n to\n\nCountry:%s\nSea:%s\n", Res->Shipname /*Res->Shipname ne podxodit t.k. nelz9 Res->Shipname=search->str1*/, Res->port1.str2, Res->port1.str3, Res->port2.str2, Res->port2.str3);
		fprintf(fout, "\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
	}
	printf("All ship info was successfully copied to Shipsoutput.dat\n");
	printf("press any key to return to menu");
	fflush(stdin);
	char str[1];
	gets(str);
	fclose(f1);
	fclose(f2);
	fclose(fout);
}
