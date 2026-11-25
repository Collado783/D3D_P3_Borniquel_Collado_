using UnityEngine;
using UnityEngine.UI;
public class UICoinManager : MonoBehaviour
{
    public Sprite Zero;
    public Sprite One;
    public Sprite Two;
    public Sprite Three;
    public Sprite Four;
    public Sprite Five;
    public Sprite Six;
    public Sprite Seven;
    public Sprite Eight;
    public Sprite Nine;

    public Image CanvasImage;
    public Image CanvasImage2;
    public MarioController m_Player;

    private void Start()
    {
        m_Player = GetComponent<MarioController>();
        CanvasImage2.gameObject.SetActive(false);
    }

    void Update()
    {
        //if(m_Player.Score==0){CanvasImage.sprite= Zero;}
        //else if(m_Player.Score==1){CanvasImage.sprite= One;}
        //else if(m_Player.Score==2){CanvasImage.sprite= Two;}
        //else if(m_Player.Score==3){CanvasImage.sprite= Three;}
        //else if(m_Player.Score==4){CanvasImage.sprite= Four;}
        //else if(m_Player.Score==5){CanvasImage.sprite= Five;}
        //else if(m_Player.Score==6){CanvasImage.sprite= Six;}
        //else if(m_Player.Score==7){CanvasImage.sprite= Seven;}
        //else if(m_Player.Score==8){CanvasImage.sprite= Eight;}
        //else if(m_Player.Score==9){CanvasImage.sprite= Nine;}
        //else if(m_Player.Score==10){CanvasImage.sprite= One; CanvasImage2.gameObject.SetActive(false); CanvasImage2.Sprite = Zero;}
        //else if(m_Player.Score==11){CanvasImage.sprite= One; CanvasImage2.Sprite = One;}
        //else if(m_Player.Score==12){CanvasImage.sprite= One; CanvasImage2.Sprite = Two;}
        //else if(m_Player.Score==13){CanvasImage.sprite= One; CanvasImage2.Sprite = Three;}
        //else if(m_Player.Score==14){CanvasImage.sprite= One; CanvasImage2.Sprite = Four;}
        //else if(m_Player.Score==15){CanvasImage.sprite= One; CanvasImage2.Sprite = Five;}
        //else if(m_Player.Score==16){CanvasImage.sprite= One; CanvasImage2.Sprite = Six;}
        //else if(m_Player.Score==17){CanvasImage.sprite= One; CanvasImage2.Sprite = Seven;}
        //else if(m_Player.Score==18){CanvasImage.sprite= One; CanvasImage2.Sprite = Eight;}
        //else if(m_Player.Score==19){CanvasImage.sprite= One; CanvasImage2.Sprite = Nine;}
        //else if(m_Player.Score==20){CanvasImage.sprite= Two; CanvasImage2.Sprite = Zero;}
    }
}
