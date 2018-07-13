using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudioSource;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;


    private string fireButton;
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;


    private void OnEnable()
    {
        currentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }

    private void Start()
    {
        fireButton = "Fire" + m_PlayerNumber;

        chargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }


    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce;

        if (currentLaunchForce >= m_MaxLaunchForce && !fired)
        {
            currentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        else if (Input.GetButtonDown(fireButton))
        {
            fired = false;
            currentLaunchForce = m_MinLaunchForce;
            m_ShootingAudioSource.clip = m_ChargingClip;
            m_ShootingAudioSource.Play();
        }
        else if (Input.GetButton(fireButton) && !fired)
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;
            m_AimSlider.value = currentLaunchForce;
        }
        else if (Input.GetButtonUp(fireButton) && !fired)
        {
            Fire();
        }


    } // Update() ///


    private void Fire()
    {
        // Instantiate and launch the shell.
        fired = true;
        Rigidbody shellInstace = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstace.velocity = currentLaunchForce * m_FireTransform.forward;

        m_ShootingAudioSource.clip = m_FireClip;
        m_ShootingAudioSource.Play();

        currentLaunchForce = m_MinLaunchForce;

    } // Fire() ////
}