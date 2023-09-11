<template>
  <div class="container-fluid dsp-content-container">
    <n-tabs type="line" animated>
      <n-tab-pane name="chart" tab="Chart" display-directive="show:lazy">
        Graphic
      </n-tab-pane>
      <n-tab-pane name="theory" tab="Theory" display-directive="show:lazy">
        <div class="p-3 theory-content-card">
          <h4>Спектр сигнала</h4>
          <p>Из предыдущей части вы узнали, что <em>сигнал</em> - это физический процесс во времени, параметры которого
            изменяются в соответствии с передаваемым сообщением. Мы научились представлять дискретные (цифровые) сигналы
            во времени. В этом разделе будет показан переход между временной и частотной областями для дискретных
            сигналов.</p>
          <h4>Прямое преобразование Фурье</h4>
          <p>Чтобы преобразовать сигнал из временной области в частотную и обратно необходимо выполнить операцию под
            названием дискретное <strong>преобразование Фурье</strong>.</p>
          <p>Запишем формулу прямого преобразования Фурье для дискретной последовательности x(nT). Прямым дискретным
            преобразованием Фурье (ДПФ) называется преобразование последовательности <code>x(n), n = 0, ... , N–1</code> в
            последовательность <code>X(k), k = 0, ..., N–1</code> по следующей формуле:</p>
          <equation-wrapper v-model="equation.e_2_1"></equation-wrapper>
          <p>где <equation-wrapper v-model="localEquation.e_1"></equation-wrapper></p>
          <ul>
            <li>N – количество компонент разложения, число измеренных за период значений сигнала;</li>
            <li>n – номер отсчета дискретизированного сигнала, n = 0,1, ... , N–1;</li>
            <li>k – номер гармоники компонента преобразования, а T — период времени, в течение которого брались входные
              данные;</li>
            <li><equation-wrapper v-model="localEquation.e_2"></equation-wrapper> – поворотный множитель.</li>
          </ul>
          <p>В этой формуле <equation-wrapper v-model="localEquation.e_3"></equation-wrapper> является спектральной
            плотностью (спектром) дискретной последовательности. Выражение для спектра дискретной последовательности можно
            найти, заменив в её Z-форме переменную <equation-wrapper v-model="localEquation.e_4"></equation-wrapper>
          </p>
          <p>Для аналоговых сигналов выражение суммы превращается в интеграл.</p>
          <p>Используя <strong>формулу Эйлера</strong></p>
          <equation-wrapper v-model="equation.e_2_2"></equation-wrapper>
          <p>можно определить <em>вещественную</em> и <em>мнимую</em> составляющие, а также <strong>модуль</strong> и
            <strong>аргумент</strong> спектральной плотности, которые связаны с вещественной и мнимой частями спектра
            через формулы теории функции комплексного переменного.
          </p>
          <p>Модуль:</p>
          <equation-wrapper v-model="equation.e_2_3"></equation-wrapper>
          <p>Фаза:</p>
          <equation-wrapper v-model="equation.e_2_4"></equation-wrapper>
          <p>Таким образом, ДПФ для N
            входных отсчетов сигнала ставит в соответствие N спектральных отсчётов. Из формулы ДПФ для вычисления одного
            спектрального отсчета требуется N операций комплексного умножения и сложения. Поскольку таких операций N
            , то общая вычислительная сложность ДПФ равна <equation-wrapper
              v-model="localEquation.e_5"></equation-wrapper></p>
          <h4>Обратное преобразование Фурье</h4>
          <p><strong>Обратное дискретное преобразование Фурье</strong> (ОДПФ) есть перевод последовательности
            <code>X(k), k = 0, ..., N–1</code> в последовательность <code>x(n), n = 0, ... , N–1</code> по формуле:
          </p>
          <equation-wrapper v-model="equation.e_2_5"></equation-wrapper>
          <p>где <code>x(n)</code> – измеренная последовательность в дискретных временных точках, значения которой
            являются исходными данными для прямого преобразования и выходными для обратного
            <code>X(k)</code> – N–последовательность комплексных амплитуд синусоидальных сигналов, образующих исходный
            сигнал <code>x(n)</code>; значения последовательности являются выходными данными для <em>прямого</em>
            преобразования и входными для <em>обратного</em>
          </p>
          <p>Поскольку амплитуды спектральных отсчетов - комплексные величины, то по ним можно вычислить одновременно и
            амплитуду, и фазу сигнала.</p>
          <hr>
          <p>Как следует из теоремы <strong>Найквиста-Котельникова</strong>, ДПФ точно соответствует непрерывному
            преобразованию Фурье, если преобразуемая функция есть функция с ограниченным спектром, при этом частота
            дискретизации <strong>Fд</strong> должна быть не меньше удвоенной максимальной частоты спектра
            <strong>Fв</strong>. Следует отметить, что для ДПФ справедливы правила и свойства, которые были рассмотрены
            для Z-преобразования. Матрицей k∗n
            элементов можно определить ДПФ.
          </p>
          <hr>
          <h4>Особенности спектров дискретных сигналов.</h4>
          <ol>
            <li>
              <p>Спектральная плотность дискретного сигнала – периодическая функция с периодом, равным частоте
                дискретизации.</p>
            </li>
            <li>
              <p>Если дискретная последовательность <strong>вещественная</strong>, то модуль спектральной плотности такой
                последовательности есть <strong>четная</strong> функция, а аргумент – <strong>нечетная</strong> функция
                частоты.</p>
            </li>
            <li>
              <p>При сдвиге спектра <equation-wrapper v-model="localEquation.e_6"></equation-wrapper> последовательности
                <equation-wrapper v-model="localEquation.e_7"></equation-wrapper> по оси частот вправо на величину
                <equation-wrapper v-model="localEquation.e_8"></equation-wrapper> получим спектр <equation-wrapper
                  v-model="localEquation.e_9"></equation-wrapper> Такому спектру соответствует комплексная
                последовательность:
              </p>
              <equation-wrapper v-model="equation.e_2_6"></equation-wrapper>
              <p>Сдвиг спектра <equation-wrapper v-model="localEquation.e_6"></equation-wrapper></p>последовательности
              <equation-wrapper v-model="localEquation.e_7"></equation-wrapper> по оси частот влево на величину
              <equation-wrapper v-model="localEquation.e_8"></equation-wrapper> происходит путем умножения
              последовательности <equation-wrapper v-model="localEquation.e_7"></equation-wrapper> на комплексную
              экспоненту <equation-wrapper v-model="localEquation.e_10"></equation-wrapper> на комплексную экспоненту
            </li>
          </ol>
        </div>
      </n-tab-pane>
    </n-tabs>
  </div>
</template>

<script>
import { NTabs, NTabPane } from "naive-ui";
import 'katex/dist/katex.css';

import EquationWrapper from "./EquationWrapper.vue"
import { TheoryEquations as equation } from "@/content/theoryEquations";

export default {
  components: {
    NTabs,
    NTabPane,
    EquationWrapper
  },
  data() {
    return {
      equation,
      localEquation: {
        e_1: `k = 0, ..., N-1`,
        e_2: `W = e^{-2\\pi j / N}`,
        e_3: `X(kT) = X(e^{j\\omega T})`,
        e_4: `z = e^{j\\omega T}`,
        e_5: `N^2`,
        e_6: `X(e^{j\\omega T})`,
        e_7: `x(nT)`,
        e_8: `\\psi`,
        e_9: `Y(e^{j\\omega T}) = Y(e^{j(\\omega-\\psi) T})`,
        e_10: `e^{j\\psi nT}`,
        e_11: ``,
        e_12: ``,
        e_13: ``,
        e_14: ``,
        e_15: ``,
      }
    };
  },
  computed: {},
  mounted() {
  },
  methods: {
  },
};
</script>
