#include<stdio.h>

/*
The Rules:
Card A has a higher rank if the face of card A is greater than that of card B.
If Cards A and B have the same face, the card with a higher level of suit has a higher rank.

Face:2<3<4<5<6<7<8<9<10<J<Q<K<A
Suit: S<D<H<C
*/

struct card {
   int face, suit;
};

int higher(struct card A, struct card B){
    if ( A.face > B.face ) return 1;
    else{
        if ( A.suit > B.suit ) return 1;
    }
    return 0;
}

struct card input_card(){
    struct card bufferCard;
    int face = 0;
    int suit = 0 , cc = 0;
    if ( scanf("%i",&face) == 1 ) bufferCard.face = face;
    else{
        scanf("%c",&cc);
        switch ( cc ){
        case 'J':
            bufferCard.face = 11;
            break;
        case 'Q':
            bufferCard.face = 12;
            break;    
        case 'K':
            bufferCard.face = 13;
            break;
        case 'A':
            bufferCard.face = 14;
            break;
        }
    }
    suit = getchar();
    switch ( suit ){
        case 'S':
            bufferCard.suit = 0;
            break;
        case 'D':
            bufferCard.suit = 1;
            break;    
        case 'H':
            bufferCard.suit = 2;
            break;
        case 'C':
            bufferCard.suit = 3;
            break;
        }
    return bufferCard;
}

// CANNOT modify!
int main(void){
    struct card A, B;
    int num;
    scanf("%d",&num);
    while(num > 0) {
        A = input_card();
        B = input_card();
        printf("%d\n", higher(A,B));
        num--;
     }
    return 0;
}