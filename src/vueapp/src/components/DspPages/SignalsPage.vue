<template>
  <div class="container-fluid dsp-content-container">
    <div class="row">
      <div clas="col-auto">
        <n-button @click="showTheory = true" type="success" ghost>
          Show theory
        </n-button>
      </div>
    </div>
    <n-modal v-model:show="showTheory" class="w-75" preset="card" :style="bodyStyle" title="Сигналы" :bordered="true"
      size="huge">
      <div class="theory-content-card">
        <p>
          <em>Сигналом</em> называется физический процесс, параметры которого изменяются в соответствии с передаваемым
          сообщением.
          Cигнал является материальным носителем информации. По способу представления сигналы разделяются на две
          группы
          –
          <em>случайные</em> и <em>детерминированные</em>. Их описывают математической моделью или функцией,
          характеризующей изменение
          параметров сигнала.
        </p>
        <p>
          <em>Случайным</em> сигналом называют функцию времени, значения которой заранее неизвестны и могут быть
          предсказаны
          лишь
          с
          некоторой <em>вероятностью</em>. К основным характеристикам случайных сигналов относятся:
        </p>
        <ul>
          <li>закон распределения (относительное время пребывания значения сигнала в определенном интервале),</li>
          <li> спектральное распределение мощности.</li>
        </ul>
        <p>
          <em>Детерминированные</em> сигналы описываются аналитической функцией (задаются аналитически), и их поведение
          полностью
          известно в любой момент времени. Детерминированные сигналы в свою очередь бывают <em>периодическими</em> и
          <em>непериодическими</em>. Непериодические сигналы, как правило, ограничены во времени.
        </p>
        <p><em>Периодический</em> сигнал - это сигнал, который повторяется во времени с определенным периодом, то есть для
          которого выполняется условие:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>где <em>k</em> – любое целое число, <em>T</em> – период повторения.</p>
        <p>Пример <em>периодического</em> сигнала – гармоническое колебание, описываемое следующим выражением:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>Известно, что любой сложный периодический сигнал может быть представлен в виде суммы гармонических колебаний с
          частотами, кратными основной частоте <n-config-provider :katex="katex">
            <n-equation :value="equation" :katex-options="katexOptions" />
          </n-config-provider></p>
        <h4>Цифровые сигналы</h4>
        <p>Все сигналы можно разделить на четыре группы:</p>
        <ul>
          <li>аналоговые,</li>
          <li>дискретные,</li>
          <li>квантованные,</li>
          <li>цифровые.</li>
        </ul>
        <p><strong>Аналоговый</strong> сигнал – описывается непрерывной функцией времени. Аналоговый сигнал обеспечивает
          передачу данных путем непрерывного изменения во времени амплитуды, частоты или фазы. Практически все физические
          процессы описываются непрерывными функциями времени, поэтому представляют собой аналоговые сигналы. Для
          аналогового сигнала область значений и определения описывается <em>непрерывным множеством</em>.</p>
        <p>Для <strong>дискретного</strong> сигнала свойственно прерывистое (дискретное) изменение сигнала во времени. То
          есть изменения в сигнале происходят скачкообразно через некоторые промежутки времени, называемые интервалом
          дискретизации – Δt или Td. Дискретизация <em>аналогового сигнала</em> состоит в том, что сигнал представляется в
          виде последовательности значений, взятых в дискретные моменты времени, которые называются <em>отсчётами</em>
          (сэмплами).</p>
        <p>Для правильного восстановления аналогового сигнала из цифрового без искажений и потерь используется теорема
          отсчетов, известная как <strong>теорема Котельникова (Найквиста-Шеннона)</strong>.</p>
        <blockquote>
          <p>Любой непрерывный сигнал с ограниченным спектром может быть восстановлен однозначно и без потерь по своим
            дискретным отсчетам, взятым с частотой строго больше удвоенной верхней частоты спектра непрерывного сигнала.
          </p>
        </blockquote>
        <p>Формула теоремы Котельникова:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>где</p>
        <ul>
          <li>F<sub>s</sub> - частота дискретизации сигнала,</li>
          <li>F<sub>a</sub> - верхняя частота спектра аналогового сигнала.</li>
        </ul>
        <p>Такое определение относится к функциям времени, которые состоят из частот от нуля до <n-config-provider
            :katex="katex">
            <n-equation :value="equation" :katex-options="katexOptions" />
          </n-config-provider></p>
        <p>В реальных задачах в радиотехнике спектр сигнала может лежать в любом диапазоне частот и начинаться и
          заканчиваться на любой частоте, в связи с этим определение Теоремы Котельникова правильно рассматривать
          относительно ширины спектра такого сигнала:</p>
        <blockquote>
          <p>Любой непрерывный сигнал с ограниченным спектром может быть восстановлен однозначно и без потерь по своим
            дискретным отсчетам, взятым с частотой строго больше удвоенной ширины полосы частот, занимаемой спектром
            непрерывного сигнала.</p>
        </blockquote>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>где <n-config-provider :katex="katex">
            <n-equation :value="equation" :katex-options="katexOptions" />
          </n-config-provider> - ширина спектра непрерывного сигнала.
        </p>
        <p><strong>Квантованные</strong> сигналы принимают ряд конечных значений из диапазона непрерывных или дискретных
          величин. Как правило, сигналы квантуются по уровню, то есть по амплитуде.</p>
        <p><strong>Цифровые</strong> сигналы получаются из аналоговых с помощью операций <strong>дискретизации</strong> и
          <strong>квантования</strong> по уровню. Значениям цифрового сигнала присваивается кодовое слово или набор
          символов (зачастую двоичных).
        </p>
        <p>Устройства, осуществляющие дискретизацию по времени и квантование по уровню, называются
          <strong>аналого-цифровыми преобразователями (АЦП)</strong>.
          Устройства, переводящие цифровой сигнал в аналоговый называются <strong>цифро-аналоговыми преобразователями
            (ЦАП)</strong>.
        </p>
        <h4>Дискретные последовательности</h4>
        <p><strong>Дискретной последовательностью</strong> называется математическая модель дискретного сигнала,
          представляющая собой решетчатую функцию: x(nT) = x(n), где <em>T</em> – интервал дискретизации, <em>n</em> = 0,
          1, 2, ..., N-1 - отсчёты или сэмплы.</p>
        <p>Пример конечной дискретной последовательности <code>x(nT) = {2, 1, -2, 0, 2, 3, 1, 0}</code>. Данная
          последовательность выглядит, как показано на рисунке:</p>
        <h4>Дельта-функция</h4>
        <p><strong>Единичный импульс</strong> – простейшая дискретная последовательность. Это дискретная δ-функция δ(nT)
          <em>Дирака</em>, которая равна единице при n = 0 и нулю при всех остальных значениях n.
        </p>
        <p>Дискретная δ-функция, смещенная во времени на <strong>m</strong> тактовых интервалов, записывается следующим
          образом: <code>δ(nT-mT)</code>.</p>
        <h4>Единичный скачок</h4>
        <p>Единичный скачок или <strong>функция Хевисайда</strong> – еще один вид простых и важных дискретных
          последовательностей. Он принимает нулевые значения в отрицательной области времени и единичные значения в
          положительной.</p>
        <p>Математически функция единичного скачка записывается как:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>Дискретный единичный скачок <code>σ(n)</code> и дискретная δ-функция <code>δ(n)</code> связаны между собой
          следующими соотношениями:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>Произвольная дискретная последовательность может быть записана в виде взвешенной суммы дельта-функций:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>Последовательность x(nT) называется периодической, если она удовлетворяет условию: <n-config-provider
            :katex="katex">
            <n-equation :value="equation" :katex-options="katexOptions" />
          </n-config-provider>, где</p>
        <ul>
          <li>m и N – целые числа, m = 0, 1, 2, ..., NT</li>
          <li><em>NT</em> – период дискретной последовательности.</li>
        </ul>
        <h4>Z-преобразование</h4>
        <p>Дискретные последовательности очень удобно описывать с помощью <strong>Z-формы</strong> или Z-преобразований.
        </p>
        <p><em>Z-преобразование</em> – аналог преобразования Лапласа в дискретной форме. Для дискретной последовательности
          x(nT) одностороннее Z-преобразование определяется следующим рядом:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>где <n-config-provider :katex="katex">
            <n-equation :value="equation" :katex-options="katexOptions" />
          </n-config-provider> - комплексная функция, X(z) - это Z-форма последовательности x(nT).</p>
        <p>Z-преобразование связано с преобразованием Лапласа через формулу:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <h4>Свойства Z-преобразования</h4>
        <ol>
          <li><strong>Линейность</strong></li>
        </ol>
        <p>Если последовательность x(nT) можно представить в виде линейной комбинации <n-config-provider :katex="katex">
            <n-equation :value="equation" :katex-options="katexOptions" />
          </n-config-provider></p>
        <p> то <n-config-provider :katex="katex">
            <n-equation :value="equation" :katex-options="katexOptions" />
          </n-config-provider></p>
        <p>Иными словами, Z-преобразование суммы сигналов равно сумме z-образов этих сигналов.</p>
        <ol start="2">
          <li><strong>Задержка (сдвиг по времени)</strong></li>
        </ol>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>Задержка входного сигнала на m вносит добавочный множитель <n-config-provider :katex="katex">
            <n-equation :value="equation" :katex-options="katexOptions" />
          </n-config-provider></p>
        <ol start="3">
          <li><strong>Свертка</strong></li>
        </ol>
        <p>Для последовательности y(nT) свертка двух последовательностей равна:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>или</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>А для Z-формы:</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
        <p>Z-преобразование свёртки сигналов равно произведению их Z-образов.</p>
        <p>Если входной сигнал x(nT) представить в виде взвешенной суммы δ-функций, то Z-форма принимает вид</p>
        <n-config-provider :katex="katex">
          <n-equation :value="equation" :katex-options="katexOptions" />
        </n-config-provider>
      </div>
    </n-modal>
    <div class="col">
    </div>
  </div>
</template>

<script>
import { NButton, NModal, NConfigProvider, NEquation } from "naive-ui";
import katex from 'katex'
import 'katex/dist/katex.css'

export default {
  components: {
    NButton,
    NModal,
    NConfigProvider,
    NEquation
  },
  data() {
    return {
      showTheory: false,
      katex,
      katexOptions: {
        displayMode: true,
        fleqn: true,
        macros: {
          '\\f': '#1f(#2)'
        }
      },
      equation: `z = Re(z) + j \\cdot Im(z)`,
    };
  },
  computed: {},
  methods: {
  },
};
</script>

<!-- <style>
@import "../node_modules/katex/dist/katex.min.css";
</style> -->
