using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public SpriteRenderer spriteRenderer;

        // 0 = moves with world, 1 = locked to camera
        // eg: 0.9 for far, 0.2 for near
        [Range(0f, 1f)] 
        public float parallaxFactor;

        public float spriteWidth;
        public float startPositionX;
    }

    Transform cameraTransform;

    [Header("Background Layers")]
    public ParallaxLayer[] layers;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        foreach (ParallaxLayer layer in layers)
        {
            if (layer.spriteRenderer == null) continue;

            layer.startPositionX = layer.spriteRenderer.transform.position.x;
            // Divide by 3 because each SpriteRenderer's tiled width is tripled
            layer.spriteWidth = layer.spriteRenderer.bounds.size.x / 3f;
        }
    }

    public void UpdateBackground()
    {
        foreach (ParallaxLayer layer in layers)
        {
            if (layer.spriteRenderer == null) continue;

            Transform layerTransform = layer.spriteRenderer.transform;

            float temp = cameraTransform.position.x * (1 - layer.parallaxFactor);
            float distance = cameraTransform.position.x * layer.parallaxFactor;

            // Update layer position
            layerTransform.position = new Vector3(
                layer.startPositionX + distance, 
                layerTransform.position.y, 
                layerTransform.position.z
            );

            // Infinitely loop the layer
            if (temp > layer.startPositionX + layer.spriteWidth)
            {
                layer.startPositionX += layer.spriteWidth;
            }
            else if (temp < layer.startPositionX - layer.spriteWidth)
            {
                layer.startPositionX -= layer.spriteWidth;
            }
        }
    }
}
