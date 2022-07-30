import { shallowMount } from "@vue/test-utils";
import SolarButton from "@/components/SolarButton";

describe("SolarButton.vue", () => {
  it("displays text in default slot position", () => {
    const wrapper = shallowMount(SolarButton, {
      slots: {
        default: "click here!",
      },
    });
    expect(wrapper.find("button").text()).toBe("click here!");
  });

  it("has underlying disabled button when disabled passed as prop", () => {
    const wrapper = shallowMount(SolarButton, {
      props: {
        disabled: true,
      },
      slots: {
        default: "foo",
      },
    });
    expect(wrapper.find("input:disabled"));
  });

  it("has underlying enable button when disabled passed false", () => {
    const wrapper = shallowMount(SolarButton, {
      props: {
        disabled: false,
      },
      slots: {
        default: "foo",
      },
    });
    expect(!wrapper.find("input:disabled"));
  });
});
