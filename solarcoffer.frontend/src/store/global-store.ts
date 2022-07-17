import { make } from "vuex-pathify";
import { InventoryService } from "@/services/InventoryService";
import { IInventoryTimeline } from "@/types/InventoryGraph";

class GlobalStore {
  snapshotTimeline = {
    productInventorySnapshots: [],
    timeline: [],
  };

  isTimelineBuilt = false;
}

const state = new GlobalStore();
const mutations = make.mutations(state);

const actions = {
  async assignSnapshots({ commit }: { commit: any }) {
    const inventoryService = new InventoryService();
    const res = await inventoryService.getSnapshotHistory();

    const timeline: IInventoryTimeline = {
      productInventorySnapshots: res.productInventorySnapshots,
      timeline: res.timeline,
    };

    commit("SET_SNAPSHOT_TIMELINE", timeline);
    commit("SET_IS_TIMELINE_BUILT", true);
  },
};

const getters = {};

export default {
  state,
  mutations,
  actions,
  getters,
};
