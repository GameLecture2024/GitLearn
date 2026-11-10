namespace DiscountCalculator;

/// <summary>
/// 장바구니 금액과 할인을 계산하는 서비스.
///
/// [강의용 주의] 이 클래스에는 엣지 케이스 버그가 "의도적으로" 심어져 있습니다.
/// 각 차시에서 실패하는 테스트로 버그를 재현한 뒤, PR 단위로 하나씩 고쳐 나갑니다.
/// </summary>
public class DiscountService
{
    /// <summary>
    /// 총액을 수량으로 나눠 개당 단가를 구한다.
    /// </summary>
    /// <remarks>
    /// [엣지 케이스 #1 — 2차시] quantity 가 0이면 DivideByZeroException 이 발생한다.
    /// </remarks>
    public decimal CalculateUnitPrice(decimal totalAmount, int quantity)
    {
        return totalAmount / quantity;
    }

    /// <summary>
    /// 할인율(0.0 ~ 1.0)을 적용한 최종 금액을 구한다.
    /// </summary>
    /// <remarks>
    /// [엣지 케이스 #2 — 3차시] 음수 금액 / 1을 초과하는 할인율 등을 검증하지 않는다.
    /// </remarks>
    public decimal CalculateFinalPrice(decimal totalAmount, decimal discountRate)
    {
        return totalAmount * (1 - discountRate);
    }
}
