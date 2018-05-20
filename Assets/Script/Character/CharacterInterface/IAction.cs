using System;

public interface IAction
{
	/// <summary>
	/// 피격 true - 사망 판정
	/// </summary>
	/// <returns><c>true</c>, if action was hit, <c>false</c> otherwise.</returns>
	/// <param name="team">Team.</param>
	/// <param name="damage">Damage.</param>
	bool hitAction (TYPE_TEAM team, IBullet bullet);
	//bool hitAction (TYPE_TEAM team, int damage);
}


