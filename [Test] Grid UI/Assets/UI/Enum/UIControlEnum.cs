    public enum UIControlEnum{
        UP, DOWN, LEFT, RIGHT, ACTION, RETURN, NOT_PRESSED
    }

    public static class UIControlEnumMethods{
        public static bool IsVerticalMovement(this UIControlEnum movementEnum){
            string enumName = movementEnum.ToString();
            return enumName == UIControlEnum.UP.ToString() || enumName == UIControlEnum.DOWN.ToString();
        }
        public static bool IsHorizontalMovement(this UIControlEnum movementEnum){
            string enumName = movementEnum.ToString();
            return enumName == UIControlEnum.LEFT.ToString() || enumName == UIControlEnum.RIGHT.ToString();
        }
    }