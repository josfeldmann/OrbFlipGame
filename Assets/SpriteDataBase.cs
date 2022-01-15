using UnityEngine;

[CreateAssetMenu(menuName = "SpriteDataBase")]
public class SpriteDataBase : ScriptableObject {

    public static Sprite CARDBACK, CARDFRONT, BALL, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, ZERO;
    public static Sprite ONESMALL, TWOSMALL, THREESMALL, FOURSMALL, FIVESMALL, SIXSMALL, SEVENSMALL, EIGHTSMALL, NINESMALL, ZEROSMALL;

    public Sprite cardback, cardfront, ball, one, two, three, four, five, six, seven, eight, nine, zero;
    public Sprite onesmall, twosmall, threesmall, foursmall, fivesmall, sixsmall, sevensmall, eightsmall, ninesmall, zerosmall;



    public void SetSprites() {
        CARDBACK = cardback;
        CARDFRONT = cardfront;
        BALL = ball;
        ONE = one;
        TWO = two;
        THREE = three;
        FOUR = four;
        FIVE = five;
        SIX = six;
        SEVEN = seven;
        EIGHT = eight;
        NINE = nine;
        ZERO = zero;
        ONESMALL = onesmall; TWOSMALL = twosmall; THREESMALL = threesmall;  FOURSMALL = foursmall; FIVESMALL = fivesmall; SIXSMALL = sixsmall; SEVENSMALL = sevensmall; EIGHTSMALL = eightsmall; NINESMALL = ninesmall; ZEROSMALL = zerosmall;
    }


    public static Sprite GetNumberSprite(int i) {
        if (i == 0) {
            return ZERO;
        } else if (i == 1) {
            return ONE;
        }else if (i == 2) {
            return TWO;
        } else if (i == 3) {
            return THREE;
        } else if (i == 4) {
            return FOUR;
        } else if (i == 5) {
            return FIVE;
        } else if (i == 6) {
            return SIX;
        } else if (i == 7) {
            return SEVEN;
        } else if (i == 8) {
            return EIGHT;
        } else if (i == 9) {
            return NINE;
        }
        return ZERO;

    }


    public static Sprite GetNumberSpriteSmall(int i) {
        if (i == 0) {
            return ZEROSMALL;
        } else if (i == 1) {
            return ONESMALL;
        } else if (i == 2) {
            return TWOSMALL;
        } else if (i == 3) {
            return THREESMALL;
        } else if (i == 4) {
            return FOURSMALL;
        } else if (i == 5) {
            return FIVESMALL;
        } else if (i == 6) {
            return SIXSMALL;
        } else if (i == 7) {
            return SEVENSMALL;
        } else if (i == 8) {
            return EIGHTSMALL;
        } else if (i == 9) {
            return NINESMALL;
        }
        return ZEROSMALL;

    }

}
