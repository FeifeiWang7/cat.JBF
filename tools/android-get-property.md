getprop - get property via the android property service

For example, check if Huawei 3X uses MTK6592

    getprop | grep platform   

Example output

    shell@hwG750-T01:/ $ getprop | grep platform                                   
    [ro.board.platform]: [huawei82_cwet_kk]
    [ro.config.mtk_platform_apn]: [true]
    [ro.config.mtk_platform_charge]: [charging]
    [ro.fota.platform]: [MTK_KK]
    [ro.mediatek.platform]: [MT6592]
