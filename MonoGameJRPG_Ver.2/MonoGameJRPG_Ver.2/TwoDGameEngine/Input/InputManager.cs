using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG.TwoDGameEngine.Input
{
    /// <summary>
    /// Provides utility Methods relevant to Input.
    /// </summary>
    public static class InputManager
    {
        private static KeyboardState _currentKeyboardState;
        private static KeyboardState _previousKeyboardState;

        private static GamePadState _currentGamePadState;
        private static GamePadState _previousGamePadState;

        private static MouseState _previousMouseState;
        private static MouseState _currentMouseState;

        /// <summary>
        /// Call this method before you'r Key operation(s).
        /// </summary>
        public static void UpdateCurrentStates()
        {
            _currentKeyboardState = Keyboard.GetState();

            _currentGamePadState = GamePad.GetState(PlayerIndex.One);

            _currentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Call this method after you'r key operation(s).
        /// </summary>
        public static void UpdatePreviousStates()
        {
            _previousKeyboardState = _currentKeyboardState;

            _previousGamePadState = _currentGamePadState;

            _previousMouseState = _currentMouseState;
        }

        #region Mouse
        /// <summary>
        /// Gets whether LeftMouseButton has been clicked (no holding).
        /// </summary>
        /// <returns></returns>
        public static bool OnLeftMouseClick()
        {
            return _previousMouseState.LeftButton == ButtonState.Released && 
                _currentMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Gets whether RightMouseButton has been clicked (no holding).
        /// </summary>
        /// <returns></returns>
        public static bool OnRightMouseClick()
        {
            return _previousMouseState.RightButton == ButtonState.Released &&
                _currentMouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Gets whether Mouse hovers the given Rectangle.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static bool IsMouseHoverRectangle(Rectangle rectangle)
        {
            return rectangle.Contains(_currentMouseState.Position);
        }
        #endregion

        #region Keyboard
        /// <summary>
        /// Gets whether given key is currently being pressed. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Gets whether given key is currently not being pressed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyUp(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Gets whether given key has initially been pressed.
        /// Key was up, is now down. (No holding)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool OnKeyDown(Keys key)
        {
            return _previousKeyboardState.IsKeyUp(key) && _currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Gets whether given key has initially been released.
        /// Key was down, is now up. (No holding)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool OnKeyUp(Keys key)
        {
            return _previousKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key);
        }
        #endregion

        #region GamePad
        /// <summary>
        /// Gets whether given button is currently being pressed. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsButtonDown(Buttons button)
        {
            return _currentGamePadState.IsButtonDown(button);
        }

        /// <summary>
        /// Gets whether given button is currently not being pressed.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool IsButtonUp(Buttons button)
        {
            return _currentGamePadState.IsButtonUp(button);
        }

        /// <summary>
        /// Gets whether given button has initially been pressed.
        /// Button was up, is now down. (No holding)
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool OnButtonDown(Buttons button)
        {
            return _previousGamePadState.IsButtonUp(button) && _currentGamePadState.IsButtonDown(button);
        }

        /// <summary>
        /// Gets whether given button has initially been released.
        /// Button was down, is now up. (No holding)
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool OnButtonUp(Buttons button)
        {
            return _previousGamePadState.IsButtonDown(button) && _currentGamePadState.IsButtonUp(button);
        }
        #endregion
    }
}
