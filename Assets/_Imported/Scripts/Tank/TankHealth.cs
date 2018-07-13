using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;


    private AudioSource explosionAudio;
    private ParticleSystem explosionParticles;
    private float currentHealth;
    private bool isDead;

    private void Awake()
    {
        explosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        explosionAudio = explosionParticles.GetComponent<AudioSource>();

        explosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        currentHealth = m_StartingHealth;
        isDead = false;

        SetHealthUI();
    }



    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        SetHealthUI();

        if (currentHealth <= 0f && !isDead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        m_Slider.value = currentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, currentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        isDead = true;
        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);

        explosionParticles.Play();
        explosionAudio.Play();
        gameObject.SetActive(false);
    }
}