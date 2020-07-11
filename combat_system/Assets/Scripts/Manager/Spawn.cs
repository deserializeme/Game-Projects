using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    /// <summary>
    /// creates a new instance of Buff
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Original"></param>
    /// <param name="Source"></param>
    /// <returns></returns>
    public static CreateNewBuff NewBuff(GameObject Target, CreateNewBuff Original, GameObject Source)
    {
        CreateNewBuff Buff = ScriptableObject.CreateInstance("CreateNewBuff") as CreateNewBuff;
        Buff = Original;
        Buff.name = Original.name;
        Buff.Target = Target;
        Buff.Source = Source;
        Buff.CurrentStacks = 1;
        Buff.SourcePowerOnCast = Source.GetComponent<Stats>().Power;

        StringFast Name = new StringFast(64);
        Name.Append(Source.name.ToString()).Append("'s ").Append(Buff.BuffName);
        Buff.BuffID = Name.ToString();

        return Buff;
    }

    /// <summary>
    /// creates a new instance of Shield
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Original"></param>
    /// <param name="Source"></param>
    /// <returns></returns>
    public static CreateNewShield NewShield(GameObject Target, CreateNewShield Original, GameObject Source)
    {
        CreateNewShield Shield = ScriptableObject.CreateInstance("CreateNewShield") as CreateNewShield;
        Shield = Original;
        Shield.name = Original.name;
        Shield.Target = Target;
        Shield.Source = Source;
        Shield.CurrentHealth = Shield.MaxHealth;
        Shield.CurrentStacks = 1;

        StringFast ShieldName = new StringFast(64);
        ShieldName.Append(Source.name.ToString()).Append("'s ").Append(Shield.BuffName);
        Shield.ShieldID = ShieldName.ToString();

        return Shield;
    }

    /// <summary>
    /// creates a new instance of DirectAttack
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Original"></param>
    /// <param name="Source"></param>
    /// <returns></returns>
    public static CreatNewDirectAttack NewDAttack(GameObject Target, CreatNewDirectAttack Original, GameObject Source)
    {
        CreatNewDirectAttack Attack = ScriptableObject.CreateInstance("CreatNewDirectAttack") as CreatNewDirectAttack;
        Attack = Original;
        Attack.name = Original.name;
        Attack.Source = Source;
        Attack.Target = Target;

        return Attack;
    }

    /// <summary>
    /// creates a new instance of DOT
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Original"></param>
    /// <param name="Source"></param>
    /// <returns></returns>
    public static CreateNewDOT NewDOT(GameObject Target, CreateNewDOT Original, GameObject Source)
    {
        CreateNewDOT DOT = Instantiate<CreateNewDOT>(Original);
        DOT.name = Original.name;
        DOT.CurrentDamage = DOT.Damage;
        DOT.Source = Source;
        DOT.Target = Target;
        DOT.CurrentStacks = 1;

        StringFast DOTName = new StringFast(64);
        DOTName.Append(Source.name.ToString()).Append("'s ").Append(DOT.DOTName);
        DOT.DOT_ID = DOTName.ToString();
        return DOT;
    }

    /// <summary>
    /// creates a new instance of of Particle, destroys it after Duration
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Source"></param>
    /// <param name="DAttack"></param>
    /// <param name="Buff"></param>
    /// <param name="Shield"></param>
    /// <param name="DOT"></param>
    /// <param name="Projectile"></param>
    public static void NewParticle(GameObject Target = null, GameObject Source = null, CreatNewDirectAttack DAttack = null, CreateNewBuff Buff = null, CreateNewShield Shield = null, CreateNewDOT DOT = null, CreateNewProjectile Projectile = null)
    {

        if(DAttack != null)
        {
            GameObject Particle = Instantiate<GameObject>(DAttack.Particle);
            Vector3 ParticlePosition = new Vector3(Target.transform.position.x, (Target.transform.position.y + (Target.transform.localScale.y / 2)), Target.transform.position.z);
            Particle.transform.position = ParticlePosition;
            Particle.transform.SetParent(Target.transform);
            Destroy(Particle, DAttack.ParticleDuration);

            if (DAttack.UseSecondaryParticle == true)
            {
                GameObject SecondaryParticle = Instantiate<GameObject>(DAttack.Secondary_Particle);
                Vector3 SecondaryParticlePosition = new Vector3(Target.transform.position.x, (Target.transform.position.y + (Target.transform.localScale.y / 2)), Target.transform.position.z);
                SecondaryParticle.transform.position = SecondaryParticlePosition;
                SecondaryParticle.transform.SetParent(Target.transform);
                Destroy(SecondaryParticle, DAttack.SecondaryParticleDuration);
            }
        }



        if (Buff != null)
        {
            GameObject Particle = Instantiate<GameObject>(Buff.Particle);
            Vector3 ParticlePosition = new Vector3(Target.transform.position.x, (Target.transform.position.y + (Target.transform.localScale.y / 2)), Target.transform.position.z);
            Particle.transform.position = ParticlePosition;
            Particle.transform.SetParent(Target.transform);
            Destroy(Particle, Buff.ParticleDuration);
        }

        if (DOT != null)
        {
            GameObject Particle = Instantiate<GameObject>(DOT.Particle);
            Vector3 ParticlePosition = new Vector3(Target.transform.position.x, (Target.transform.position.y + (Target.transform.localScale.y / 2)), Target.transform.position.z);
            Particle.transform.position = ParticlePosition;
            Particle.transform.SetParent(Target.transform);
            Destroy(Particle, DOT.ParticleDuration);
        }

        if (Shield != null)
        {
            GameObject Particle = Instantiate<GameObject>(Shield.Particle);
            Vector3 ParticlePosition = new Vector3(Target.transform.position.x, (Target.transform.position.y + (Target.transform.localScale.y / 2)), Target.transform.position.z);
            Particle.transform.position = ParticlePosition;
            Particle.transform.SetParent(Target.transform);
            Destroy(Particle, Shield.ParticleDuration);
        }

        if(Projectile != null)
        {
            GameObject Particle = Instantiate<GameObject>(Projectile.Secondary_Particle);
            Vector3 ParticlePosition = new Vector3(Target.transform.position.x, (Target.transform.position.y + (Target.transform.localScale.y / 2)), Target.transform.position.z);
            Particle.transform.position = ParticlePosition;
            Particle.transform.SetParent(Target.transform);
            Destroy(Particle, Projectile.SecondaryParticleDuration);
        }
    }

    /// <summary>
    /// creates a new instance of Projectile and launches it
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Projectile"></param>
    /// <param name="Source"></param>
    public static void NewProjectile(GameObject Target, CreateNewProjectile Projectile, GameObject Source)
    {
        GameObject Particle = Instantiate<GameObject>(Projectile.Particle);
        ParticleMover PM = ScriptableObject.CreateInstance("ParticleMover") as ParticleMover;

        PM.Source = Source;
        PM.Target = Target;
        PM.Spline = Spawn.NewSpline(Target, Projectile.Path, Source);
        PM.Projectile = Projectile;
        PM.Particle = Particle;
        PM.Launch();
    }

    /// <summary>
    /// creates a new instance of SplineProfile
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Original"></param>
    /// <param name="Source"></param>
    /// <returns></returns>
    public static CreateSplineProfile NewSpline(GameObject Target, CreateSplineProfile Original, GameObject Source)
    {
        CreateSplineProfile Spline = Instantiate<CreateSplineProfile>(Original);
        Spline.name = Original.name;
        Spline.Caster = Source;
        Spline.Victim = Target;
        return Spline;
    }
    }
