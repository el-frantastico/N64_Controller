    AREA asm_func, CODE, READONLY
    EXPORT my_wait_us_asm
    
my_wait_us_asm

WAIT_1_US
    ; According to ARM spec, NOPs may be removed by the assembler, so they
    ; are not a reliable way to eat up time. Instead we simply do empty adds
    ; to eat up clock cycles.
    
    ; The LPC1768 operates at 96 MHz. Assuming each instruction is 1 clock cycle,
    ; 96 instructions should take 1 microsecond
    
    ADD     R1, R2, #0      ; clock cycle 1
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 10
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 20
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 20
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 30
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 50
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 60
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 70
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 80
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 90
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0
    ADD     R1, R2, #0      ; clock cycle 94
    
    SUBS    R0, R0, #1      ; clock cycle 95
    BGT     WAIT_1_US       ; clock cycle 96
    
    BX      LR      ; return
    END        