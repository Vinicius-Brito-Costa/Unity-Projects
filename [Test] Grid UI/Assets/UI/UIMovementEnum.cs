    public enum UIMovementEnum{
        UP, DOWN, LEFT, RIGHT
    }

    public static class UIMovementEnumMethods{
        public static bool IsVerticalMovement(this UIMovementEnum movementEnum){
            string enumName = movementEnum.ToString();
            return enumName == UIMovementEnum.UP.ToString() || enumName == UIMovementEnum.DOWN.ToString();
        }
        public static bool IsHorizontalMovement(this UIMovementEnum movementEnum){
            string enumName = movementEnum.ToString();
            return enumName == UIMovementEnum.LEFT.ToString() || enumName == UIMovementEnum.RIGHT.ToString();
        }
    }