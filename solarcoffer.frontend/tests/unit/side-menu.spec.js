import { mount } from "@vue/test-utils";
import SideMenu from "@/components/SideMenu";

describe("SideMenu.vue", () => {
  it("render correct number of button links", () => {
    const wrapper = mount(SideMenu, {
      props: {},
      stubs: ["router-link"],
      slots: {},
    });

    expect(wrapper.findAll("button").length).toBe(4);
  });
});
