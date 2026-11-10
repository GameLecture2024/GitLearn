using DiscountCalculator;
using Xunit;

namespace DiscountCalculator.Tests;

public class DiscountServiceTests
{
    // ── 기본(happy path) 테스트 ─────────────────────────────
    // 아래 테스트들은 현재 "통과"합니다. 레포의 초록(green) 상태 기준선입니다.
    // 엣지 케이스 테스트는 각 차시에서 학생이 직접 추가하여 "빨강(red)"을 먼저 만든 뒤 고칩니다.

    [Fact]
    public void CalculateUnitPrice_나누어떨어지는_경우_단가를_반환한다()
    {
        var service = new DiscountService();

        var unitPrice = service.CalculateUnitPrice(1000m, 4);

        Assert.Equal(250m, unitPrice);
    }

    [Fact]
    public void CalculateFinalPrice_할인율을_적용한_금액을_반환한다()
    {
        var service = new DiscountService();

        var finalPrice = service.CalculateFinalPrice(1000m, 0.1m);

        Assert.Equal(900m, finalPrice);
    }

    // ── 2차시에서 추가할 "실패하는 테스트" (지금은 주석) ──────────
    // 아래 주석을 해제하면 DivideByZeroException 으로 테스트가 빨강이 됩니다.
    // 2차시: 이 빨강을 재현 → DiscountService 를 고쳐 초록으로 만든 뒤 PR 을 엽니다.
    //
    // [Fact]
    // public void CalculateUnitPrice_수량이_0이면_단가는_0이다()
    // {
    //     var service = new DiscountService();
    //
    //     var unitPrice = service.CalculateUnitPrice(1000m, 0);
    //
    //     Assert.Equal(0m, unitPrice);
    // }
}
