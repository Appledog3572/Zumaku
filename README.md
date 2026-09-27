# Zumaku — 彈幕祖瑪

## 專案簡介
彈幕祖瑪（Zumaku）是一款將經典 Zuma 彈珠消除玩法與彈幕射擊（danmaku / bullet hell）機制結合的 Unity 遊戲，目前為開發中專案。

## 核心玩法設計
移動中的 Zuma 球體本身會對玩家持續發射彈幕，玩家必須在「進行 Zuma 消除」的同時「閃避彈幕」。彈幕數量與球鏈上存活的球體數量直接掛鉤：玩家消除得越不順、球體留在鏈上越久，彈幕來源就越多，形成「消除表現直接反映在閃避壓力上」的正回饋設計，而不是額外疊加的懲罰機制。不同顏色的球體對應不同彈幕模式與威脅性，玩家需依自己擅長閃避的模式，策略性判斷優先消除哪個顏色的球體。

## 開發環境
- Unity 6000.3.24f1（Unity 6）
- URP 2D 渲染管線（Universal Render Pipeline）
- Unity Splines（球體路徑移動）
- Unity 2D Physics

## 目前完成度
目前完成的是彈幕系統的核心架構與測試場景（`TestRoom`），已實作 8 種顏色球體、6 種彈幕攻擊模式，以及玩家受擊扣血（`PlayerHealth.cs`）。

彈幕強度隨 `currentPhase` 分級（見下方〈彈幕強化效果〉）的機制已經做好，`currentPhase` 目前是手動設定的數值。「消除表現影響彈幕總量」這個核心設計則依賴 Zuma 球鏈配對／消除機制，而該機制尚未開始實作，因此這部分的正回饋效果目前還無法在遊戲中體現。專案整體仍處於原型階段，持續開發中。

## 架構設計

### 彈幕模式（Strategy Pattern）
所有彈幕邏輯透過 `BulletPatternSO`（抽象 ScriptableObject 基底類別）抽象化。每個球體（`Ball`）持有一個 `pattern` 欄位，依冷卻時間週期呼叫 `pattern.Fire()` 觸發攻擊。新增彈幕模式只需新增一個繼承類別，不須修改 `Ball.cs` 本體；同時因為是 ScriptableObject，可直接在 Unity Inspector 建立、指派資料資產，數值調整與程式邏輯脫鉤，不需要重新編譯。

目前已實作的彈幕模式：
- `StraightShotPattern`（直線射擊）
- `RandomFanPattern`（扇形散射）
- `SplitShotPattern`（分裂彈）
- `BounceShotPattern`（彈跳彈）
- `SniperShotPattern`（狙擊彈）
- `SniperLaserPattern`（狙擊雷射）

### 彈幕強化效果
部分顏色球體額外搭配強化彈幕威脅性的特殊效果，效果強度隨 `currentPhase` 分級增強：

- **RepulsionField（斥力力場）**：以 `Physics2D.OverlapCircleAll` 偵測範圍內標記為 `Bullet` 的物件，依距離做力道衰減後將其推離原本彈道，使周遭彈幕軌跡變得不規則、提高閃避難度；力場半徑與推力隨階段提升而增強。
- **BlockFog（週期性迷霧）**：定時開關的視覺遮蔽效果，遮蔽範圍隨階段增大，迫使玩家在視野受限的情況下閃避。

### 路徑移動
球體使用 Unity Splines 套件沿設計好的軌道移動（`SplineFollower.cs`），對應 Zuma 遊戲中球鏈沿固定軌道前進的核心視覺呈現。

## 待完成項目
- 球鏈生成與配對消除邏輯（Zuma 核心玩法；完成後將自然實現「未消除球體持續發射彈幕、消除表現影響彈幕總量」的核心設計）
- 發射／瞄準機制
- 分數／關卡系統

## Demo
（測試場景截圖待補）
