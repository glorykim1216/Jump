using UnityEngine;

namespace WaterBuoyancy
{
    public class WaterWaves : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1f;

        [SerializeField]
        private float height = 0.2f;

        [SerializeField]
        private float noiseWalk = 0.5f;

        [SerializeField]
        private float noiseStrength = 0.1f;

        private Mesh mesh;
        private Vector3[] baseVertices;
        private Vector3[] vertices;
        MeshRenderer meshRenderer;
        Color WaterColor;
        float ColorSum;
        float ColorH;
        bool initColor;
        protected virtual void Awake()
        {
            this.mesh = this.GetComponent<MeshFilter>().mesh;
            this.baseVertices = this.mesh.vertices;
            this.vertices = new Vector3[this.baseVertices.Length];
            this.meshRenderer = this.GetComponent<MeshRenderer>();
            WaterColor = new Color();
            ColorSum = 219;
        }

        protected virtual void Start()
        {
            this.ResizeBoxCollider();
        }

        protected virtual void Update()
        {
            //Debug.Log(meshRenderer.material.GetColor("_DepthGradientDeep"));
            //물색상 변경
            ColorH = 18f / 1000f * GameManager.Instance.distance;
            if (ColorH >= 141f)
                ColorH -= 360f;

            WaterColor =Color.HSVToRGB((219f+ColorH) / 360f, 91f/100f, 100f/100f,true);

            //Debug.Log("GameManager.Instance.distance" + GameManager.Instance.distance);

            //Debug.Log("ColorSum" +ColorSum);

            //Debug.Log("ColorH" +ColorH);
            WaterColor = new Color(WaterColor.r, WaterColor.g, WaterColor.b, 0.75f);
            meshRenderer.material.SetColor("_DepthGradientDeep", WaterColor);
            for (var i = 0; i < this.vertices.Length; i++)
            {
                var vertex = this.baseVertices[i];
                vertex.y += 
                    Mathf.Sin(Time.time * this.speed + this.baseVertices[i].x + this.baseVertices[i].y + this.baseVertices[i].z) * 
                    (this.height / this.transform.localScale.y);

                vertex.y += Mathf.PerlinNoise(baseVertices[i].x + this.noiseWalk, baseVertices[i].y /*+ Mathf.Sin(Time.time * 0.1f)*/) * this.noiseStrength;

                this.vertices[i] = vertex;
            }

            this.mesh.vertices = this.vertices;
            this.mesh.RecalculateNormals();
        }

        private void ResizeBoxCollider()
        {
            var boxCollider = this.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Vector3 center = boxCollider.center;
                center.y = boxCollider.size.y / -2f;
                center.y += (this.height + this.noiseStrength) / this.transform.localScale.y;

                boxCollider.center = center;
            }
        }
    }
}
