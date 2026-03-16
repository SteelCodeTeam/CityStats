import { ModRegistrar } from "cs2/modding";
import { CityStatsPanel } from "mods/city-stats-panel";

const register: ModRegistrar = (moduleRegistry) => {
    moduleRegistry.append("Game", CityStatsPanel);
};

export default register;