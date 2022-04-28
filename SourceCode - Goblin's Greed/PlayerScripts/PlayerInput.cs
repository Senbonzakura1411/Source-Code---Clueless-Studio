using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        
        [SerializeField] private LayerMask obstructionMask = -1;
        public Vector2 InputVector { get; private set; }
    
        public Vector2 MouseAxesInputVector { get; private set; }
        
        public Vector3 MouseWorldTerrainPositionInputVector { get; private set; }
        
        public bool MouseLeftClick { get; private set; }

        public bool MouseRightClick { get; private set; }
        
        public bool JumpInput { get; private set; }
        
        public bool RunInput { get; private set; }
        
        public bool ActionInput { get; private set; }
        
        public bool HasteInput { get; private set; }
        
        public bool CopycatInput { get; private set; }
        
        public bool InventoryInput { get; private set; }

        public bool OnUI { get; set; }
        private void Update()
        {

            if (!OnUI)
            {
                var h = Input.GetAxisRaw("Horizontal");
                var v = Input.GetAxisRaw("Vertical");
                InputVector = new Vector2(h, v);

                var ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hitData, 300, obstructionMask))
                {
                    var point = hitData.point;
                    MouseWorldTerrainPositionInputVector = point;
                }

                MouseLeftClick = Input.GetKeyDown(KeyCode.Mouse0);

                MouseRightClick = Input.GetKeyDown(KeyCode.Mouse1);

                JumpInput = Input.GetButtonDown("Jump");

                RunInput = Input.GetKey(KeyCode.LeftShift);

                ActionInput = Input.GetKey(KeyCode.E);

                HasteInput = Input.GetKeyDown(KeyCode.R);

                CopycatInput = Input.GetKeyDown(KeyCode.F);
            }
            
            var mouseY = Input.GetAxis("Mouse Y");
            var mouseX = Input.GetAxis("Mouse X");
            MouseAxesInputVector = new Vector2(mouseY, mouseX);

            InventoryInput = Input.GetKeyDown(KeyCode.I);

        }
    }
}
