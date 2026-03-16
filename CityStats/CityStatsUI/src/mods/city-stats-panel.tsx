import React from "react";
import { bindValue, trigger, useValue } from "cs2/api";

const citizenCountBinding = bindValue<number>("citystats", "citizenCount");
const serviceVehicleCountBinding = bindValue<number>("citystats", "serviceVehicleCount");
const parkedCarCountBinding = bindValue<number>("citystats", "parkedCarCount");
const movingCarCountBinding = bindValue<number>("citystats", "movingCarCount");

const StatRow = ({ label, value }: { label: string; value: number | null | undefined }) => (
    <div
        style={{
            display: "flex",
            justifyContent: "space-between",
            gap: "16rem",
            fontSize: "14rem"
        }}
    >
        <span style={{ opacity: 0.85 }}>{label}</span>
        <span style={{ fontWeight: 700 }}>{(value ?? 0).toLocaleString("es-ES")}</span>
    </div>
);

export const CityStatsPanel = () => {
    const citizenCount = useValue(citizenCountBinding);
    const serviceVehicleCount = useValue(serviceVehicleCountBinding);
    const parkedCarCount = useValue(parkedCarCountBinding);
    const movingCarCount = useValue(movingCarCountBinding);

    React.useEffect(() => {
        const id = window.setInterval(() => {
            trigger("citystats", "requestRefresh");
        }, 500);

        return () => window.clearInterval(id);
    }, []);

    return (
        <div
            style={{
                position: "fixed",
                top: "64rem",
                right: "16rem",
                minWidth: "220rem",
                padding: "12rem 16rem",
                borderRadius: "10rem",
                background: "rgba(20, 20, 28, 0.85)",
                color: "#fff",
                zIndex: 9999,
                display: "flex",
                flexDirection: "column",
                gap: "8rem"
            }}
        >
            <span style={{ fontSize: "14rem", opacity: 0.8, marginBottom: "4rem" }}>
                City Stats
            </span>

            <StatRow label="Ciudadanos" value={citizenCount} />
            <StatRow label="Vehículos de servicio" value={serviceVehicleCount} />
            <StatRow label="Coches aparcados" value={parkedCarCount} />
            <StatRow label="Coches en movimiento" value={movingCarCount} />
        </div>
    );
};