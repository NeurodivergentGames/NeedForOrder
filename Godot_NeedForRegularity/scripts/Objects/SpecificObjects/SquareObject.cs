using Godot;


namespace GameObjects

{
    public class SquareObject : RotatableObject
    {
        [Export]
        private int _lenght;
        public int Lenght
        {
            get { return _lenght; }
            private set
            {
                _lenght = value;
                _colorRect.RectSize = new Vector2(_lenght, _lenght);
            }
        }

        private ColorRect _colorRect;
        // private Label _label;


        public override void InitRandomObject()
        {
            int offset = (int)(_lenght/Mathf.Sqrt2) + 1;
            int positionX = Globals.RandomManager.rng.RandiRange(offset, (int)Globals.ScreenInfo.PlayableSize[0] - offset);
            int positionY = Globals.RandomManager.rng.RandiRange(offset, (int)Globals.ScreenInfo.PlayableSize[1] - offset);

            float angleDegrees = Globals.RandomManager.rng.RandfRange(0, 90);

            GlobalPosition = new Vector2(positionX, positionY);
            GlobalRotationDegrees = angleDegrees;
            RelevantRotationAngle = Mathf.Deg2Rad(angleDegrees);
        }

        public override void _Ready()
        {
            base._Ready();
            _colorRect = GetNode<ColorRect>("ColorRect");
            _colorRect.RectSize = new Vector2(_lenght, _lenght);
        }


        public override void _Process(float delta)
        {
            base._Process(delta);

            if (_state >= Globals.OBJECTSTATE.SELECTED)
                _colorRect.SelfModulate = new Color(0, 0, 0);
            
            else
                _colorRect.SelfModulate = new Color(1, 1, 1);

            if (_imOnRotationArea)
                _rotationArea.Modulate = new Color(0, 0, 0);
            else
                _rotationArea.Modulate = new Color(1, 1, 1);

        }


        protected override Vector2 FindPositionInPlayableArea()
        {
            float offset = _lenght/Mathf.Sqrt2;
            if (GlobalPosition.x + offset > Globals.ScreenInfo.PlayableSize.x)
            {
                return new Vector2(Globals.ScreenInfo.PlayableSize.x - offset, GlobalPosition.y);
            }
            return GlobalPosition;
        }
    }


}